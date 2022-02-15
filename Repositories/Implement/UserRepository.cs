using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories.Implement
{
    public class UserRepository : Repository<User>, IUserRepository
    {

        public UserRepository(Data.AstroDataContext dataContext) : base(dataContext)
        {
        }

        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public User GetAllUserData(int id)
        {
            var user = this.Get(id);

            AstroData.Entry(user).Collection(u => u.Profiles).Load();
            AstroData.Entry(user).Collection(u => u.Orders).Load();
            return user;
        }
    }
}
