using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Repositories.Implement
{
    public class AspectRepository : Repository<Aspect>, IAspectRepository
    {
        public AspectRepository(AstroDataContext dataContext) : base(dataContext)
        {
        }
        private AstroDataContext AstroDataContext { get { return base._context as AstroDataContext; } }

        public IEnumerable<Aspect> FindAspectWithAllData<TSortBy>(Func<Aspect, bool> filter, Func<Aspect, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20)
        {
            var aspect = AstroDataContext.Aspects
                   .Include(aspect => aspect.PlanetBase)
                   .Include(aspect => aspect.PlanetCompare)
                                           .Where(filter)
                                           .OrderBy(sortBy);
            total = aspect.Count();
            return aspect.Skip((page - 1) * pageSize).Take(pageSize + 1);
        }

        public IEnumerable<Aspect> FindAspectWithAllData<TSortBy>(Func<Aspect, bool> filter, Func<Aspect, TSortBy> sortBy, out int total)
        {
            var aspect = AstroDataContext.Aspects
                .Include(aspect => aspect.PlanetBase)
                .Include(aspect => aspect.PlanetBase)
                                        .Where(filter)
                                        .OrderBy(sortBy);
            total = aspect.Count();
            return aspect;
        }

        public Aspect GetAspectWithAllData(int id)
        {
            return AstroDataContext.Aspects
                .Include(aspect => aspect.PlanetBase)
                .Include(aspect => aspect.PlanetCompare)
                .FirstOrDefault(obj => obj.Id == id);
        }
    }
}
