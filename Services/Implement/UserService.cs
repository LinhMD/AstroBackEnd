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
    public class UserService : IUserService ,IDisposable
    {

        private readonly IUnitOfWork _work;

        private readonly AstroDataContext _astroData;
        public UserService(IUnitOfWork work, AstroDataContext astroData)
        {
            this._work = work;
            this._astroData = astroData;
        }
        public void CreateUser(UserCreateRequest user)
        {
            _work.Users.Add(new User() { 
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Status = 1
            });
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
                    checkUserName = user.UserName.Equals(userRequest.Name);
                }

                if (!string.IsNullOrWhiteSpace(userRequest.Phone))
                {
                    checkPhoneNumber = user.PhoneNumber.Equals(userRequest.Phone);
                }


                checkStatus = user.Status == userRequest.Status;
                

                return checkUserName && checkStatus && checkPhoneNumber;
            };



            IEnumerable<User> users = _work.Users.FindPaging<String>(filter, u => u.UserName, userRequest.Page, userRequest.PageSize);

            return users;
        }

        public User GetUser(int id)
        {
            return _work.Users.Get(id);
                
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUser()
        {
            return _work.Users.GetAll<string>(u => u.UserName);
        }
    }
}
