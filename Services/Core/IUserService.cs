using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.UserRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Core
{
    public interface IUserService 
    {
        public User GetUser(int id);

        public IEnumerable<User> GetAllUser();

        public IEnumerable<User> FindUsers(FindUserRequest userRequest);

        public IEnumerable<User> FindUsers(FindUserRequest userRequest, out int total);

        public void DeleteUser(int id);

        public void UpdateUser(int id, UserUpdateRequest request);

        public User CreateUser(UserCreateRequest user);

        public Order getCart(int userId);

        public Order AddToCart(int userId, AddToCartRequest request);
    }
}
