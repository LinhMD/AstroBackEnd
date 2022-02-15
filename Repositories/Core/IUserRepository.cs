﻿using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories.Core
{
    public interface IUserRepository : IRepository<User>
    {

        public User GetAllUserData(int id);
        
    }
}
