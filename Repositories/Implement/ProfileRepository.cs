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
    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(AstroDataContext astroData) : base(astroData)
        {

        }
        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public Profile GetProfileWithAllData(int id)
        {
            return AstroData.Profiles.Include(p => p.Zodiac).Include(p => p.BirthChart).FirstOrDefault(p => p.Id == id);
        }
    }
}
