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

namespace AstroBackEnd.Services.Implement
{
    public class UserService : IUserService , IDisposable
    {

        private readonly IUnitOfWork _work;

        private readonly AstroDataContext _astroData;
        public UserService(IUnitOfWork work, AstroDataContext astroData)
        {
            this._work = work;
            this._astroData = astroData;
        }

        public User CreateUser(UserCreateRequest request)
        {
            if (this._work.Users.Find(u => u.UserName == request.UserName, u => u.UserName).Any())
            {
                throw new ArgumentException("Username already exist");
            }

            User user = new User()
            {
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
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

        public User GetUser(int id)
        {
            User user = _work.Users.Get(id);

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

            if(request.Status != null)
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
            var cart = this._work.Orders.Find(o => o.UserId == userId && o.Status == 0, o => o.Id).FirstOrDefault();

            if(cart != null)
            {
                cart = new Order()
                {
                    
                };
            }

            return cart;
        }
    }
}
