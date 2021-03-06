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
            return AstroData.Users
                .Include(u => u.Role)
                .Include(u => u.Profiles)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderDetails)
                .FirstOrDefault(u => u.Id == id);
        }

        public override IQueryable<User> WithAllData()
        {
            return AstroData.Users
                .Include(u => u.Role)
                .Include(u => u.Profiles)
                .Include(u => u.Orders)
                    .ThenInclude(o => o.OrderDetails);
        }

    }
}
