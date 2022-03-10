using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Services.Core;
using AstroBackEnd.RequestModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstroBackEnd.RequestModels.UserRequest;

namespace AstroBackEnd.Services.Implement
{
    public class UserService : IUserService , IDisposable
    {

        private readonly IUnitOfWork _work;

        private readonly AstroDataContext _astroData;

        private readonly IProductService _productService;
        public UserService(IUnitOfWork work, AstroDataContext astroData, IProductService productService)
        {
            this._work = work;
            this._astroData = astroData;
            this._productService = productService;
        }

        public User CreateUser(UserCreateRequest request)
        {

            User user = new User()
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                AvatarLink = request.AvatarLink,                
                Status = 1,
                Role = _work.Roles.Get(2) // normal user
            };
            return _work.Users.Add(user);
        }

        public void DeleteUser(int id)
        {
            _work.Users.Remove(GetUser(id));
        }

        public IEnumerable<User> FindUsers(FindUserRequest userRequest)
        {
            Func<User, bool> filter = user =>
            {
                bool checkUserName = true;
                bool checkPhoneNumber = true;
                bool checkStatus = true;

                if (!string.IsNullOrWhiteSpace(userRequest.Name))
                {
                    checkUserName = user.UserName.Contains(userRequest.Name);
                }

                if (!string.IsNullOrWhiteSpace(userRequest.Phone))
                {
                    checkPhoneNumber = user.PhoneNumber.Contains(userRequest.Phone);
                }


                if (userRequest.Status.HasValue)
                {
                    checkStatus = user.Status == userRequest.Status;
                }

                
                return checkUserName && checkStatus && checkPhoneNumber;
            };

            IEnumerable<User> users = _work.Users.FindPaging<String>(filter, u => u.UserName, userRequest.PagingRequest.Page, userRequest.PagingRequest.PageSize);

            return users;
        }

        public IEnumerable<User> FindUsers(FindUserRequest userRequest, out int total)
        {
            Func<User, bool> filter = user =>
            {
                bool checkUserName = true;
                bool checkPhoneNumber = true;
                bool checkStatus = true;

                if (!string.IsNullOrWhiteSpace(userRequest.Name))
                {
                    checkUserName = user.UserName.Contains(userRequest.Name);
                }

                if (!string.IsNullOrWhiteSpace(userRequest.Phone))
                {
                    checkPhoneNumber = user.PhoneNumber.Contains(userRequest.Phone);
                }


                if (userRequest.Status.HasValue)
                {
                    checkStatus = user.Status == userRequest.Status;
                }


                return checkUserName && checkStatus && checkPhoneNumber;
            };

            IEnumerable<User> users = _work.Users.FindPaging<String>(filter, u => u.UserName, out total, userRequest.PagingRequest.Page, userRequest.PagingRequest.PageSize);

            return users;
        }

        public User GetUser(int id)
        {
            User user = _work.Users.GetAllUserData(id);

            if (user == null) throw new ArgumentException("User Id not found");

            return user;
        }

        public void UpdateUser(int id, UserUpdateRequest request)
        {
            var userUpdate = GetUser(id);

            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                userUpdate.UserName = request.UserName;
            }

            if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
            {
                userUpdate.PhoneNumber = request.PhoneNumber;
            }
            if (!string.IsNullOrWhiteSpace(request.AvatarLink))
            {
                userUpdate.AvatarLink = request.AvatarLink;
            }
            if (request.Status != null)
            {
                userUpdate.Status = request.Status.Value;
            }
        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public IEnumerable<User> GetAllUser()
        {
            return _work.Users.GetAll(u => u.UserName);
        }

        public Order getCart(int userId)
        {

            var cart = _work.Orders.FindWithAllInfo(o => o.UserId == userId && o.Status == 0, o => o.Id).FirstOrDefault(o => true);

            if(cart == null)
            {
                cart = new Order()
                {
                    Status = 0,
                    OrderTime = DateTime.Now,
                    UserId = userId
                };
                cart = this._work.Orders.Add(cart);
            }

            return cart;
        }

        public Order AddToCart(int userId, AddToCartRequest request)
        {
            var cart = this.getCart(userId);
            Product product = _productService.GetProductVariant(request.ProductId);

            if (product == null ) throw new ArgumentException("Product Id Not found");

            OrderDetail detail = new OrderDetail()
            {
                OrderId = cart.Id,
                ProductId = request.ProductId,
                Quantity = product.Id,
                TotalPrice = (double)(request.Quantity * product.Price)
            };
            cart.OrderDetails.Add(detail);

            this._work.OrderDetails.Add(detail);

            return cart;
        }

        
    }
}
