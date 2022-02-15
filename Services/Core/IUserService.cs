﻿using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.ViewsModel;
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

        public void DeleteUser(int id);

        public void UpdateUser(User user);

        public void CreateUser(UserCreateRequest user);

    }
}