using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Repositories.Implement
{
    public class HoroscopeItemRepository : Repository<HoroscopeItem>, IHoroscopeItemRepository
    {
        public HoroscopeItemRepository(AstroDataContext dataContext) : base(dataContext)
        {
        }

        private AstroDataContext AstroDataContext { get { return base._context as AstroDataContext; } }
        public IEnumerable<HoroscopeItem> FindHoroscopeItemWithAllDataPaging<TSortBy>(Func<HoroscopeItem, bool> filter, Func<HoroscopeItem, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20)
        {
            var horoscopeItem = AstroDataContext.HoroscopeItems
                .Include(horoscopeItem => horoscopeItem.Aspect)
                    .ThenInclude(aspect => aspect.PlanetBase)
                 .Include(horoscopeItem => horoscopeItem.Aspect)
                    .ThenInclude(aspect => aspect.PlanetCompare)
                .Include(horoscopeItem => horoscopeItem.LifeAttribute)
                                        .Where(filter)
                                        .OrderBy(sortBy);

            total = horoscopeItem.Count();
            return horoscopeItem.Skip((page - 1) * pageSize).Take(pageSize + 1);
            
        }

        public IEnumerable<HoroscopeItem> FindHoroscopeItemWithAllData<TSortBy>(Func<HoroscopeItem, bool> filter, Func<HoroscopeItem, TSortBy> sortBy, out int total)
        {
            var horoscopeItem = AstroDataContext.HoroscopeItems
                .Include(horoscopeItem => horoscopeItem.Aspect)
                    .ThenInclude(aspect => aspect.PlanetBase)
                 .Include(horoscopeItem => horoscopeItem.Aspect)
                    .ThenInclude(aspect => aspect.PlanetCompare)
                .Include(horoscopeItem => horoscopeItem.LifeAttribute)
                                        .Where(filter)
                                        .OrderBy(sortBy);

            total = horoscopeItem.Count();
            return horoscopeItem;
        }

        public HoroscopeItem GetHoroscopeItemWithAllData(int id)
        {
            return AstroDataContext.HoroscopeItems
                .Include(horoscopeItem => horoscopeItem.Aspect) 
                    .ThenInclude(aspect => aspect.PlanetBase)
                 .Include(horoscopeItem => horoscopeItem.Aspect)
                    .ThenInclude(aspect => aspect.PlanetCompare)
                .Include(horoscopeItem => horoscopeItem.LifeAttribute)
                .FirstOrDefault(obj => obj.Id == id);
        }

        public override IQueryable<HoroscopeItem> WithAllData()
        {
            return AstroDataContext.HoroscopeItems.AsQueryable()
                .Include(horoscopeItem => horoscopeItem.Aspect)
                    .ThenInclude(aspect => aspect.PlanetBase)
                 .Include(horoscopeItem => horoscopeItem.Aspect)
                    .ThenInclude(aspect => aspect.PlanetCompare)
                .Include(horoscopeItem => horoscopeItem.LifeAttribute);
        }
    }
}
