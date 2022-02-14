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

        private AstroDataContext AstroData { get { return _context as AstroDataContext; } }
    }
}
