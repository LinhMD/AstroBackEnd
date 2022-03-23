using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Repositories.Implement
{
    public class ZodiacHouseRepository : Repository<ZodiacHouse>, IZodiacHouseRepository
    {
        public ZodiacHouseRepository(Data.AstroDataContext dataContext) : base(dataContext)
        {

        }

        private AstroDataContext AstroData{ get { return base._context as AstroDataContext;  } }

        public IEnumerable<ZodiacHouse> FindPPagingZodiacHouseWithAllData<TSortBy>(Func<ZodiacHouse, bool> filter, Func<ZodiacHouse, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20)
        {
            var zodiacHouse = AstroData.ZodiacHouses
                             .Include(zodiacHouse => zodiacHouse.Zodiac)
                             .Include(zodiacHouse => zodiacHouse.House)
                             .Where(filter)
                             .OrderBy(sortBy);
            total = zodiacHouse.Count();
            return zodiacHouse.Skip((page - 1) * pageSize).Take(pageSize + 1);
        }

        public IEnumerable<ZodiacHouse> FindZodiacHouseWithAllData<TSortBy>(Func<ZodiacHouse, bool> filter, Func<ZodiacHouse, TSortBy> sortBy, out int total)
        {
            var zodiacHouse = AstroData.ZodiacHouses
                  .Include(zodiacHouse => zodiacHouse.Zodiac)
                  .Include(zodiacHouse => zodiacHouse.House)
                  .Where(filter)
                  .OrderBy(sortBy);
            total = zodiacHouse.Count();
            return zodiacHouse;
        }

        public ZodiacHouse GetZodiacHouseWithAllData(int id)
        {
            return AstroData.ZodiacHouses
                 .Include(zodiacHouse => zodiacHouse.Zodiac)
                 .Include(zodiacHouse => zodiacHouse.House)
                 .FirstOrDefault(zodiacHouse => zodiacHouse.Id == id);
        }

        public override IQueryable<ZodiacHouse> WithAllData()
        {
            return AstroData.ZodiacHouses.AsQueryable()
                             .Include(zodiacHouse => zodiacHouse.Zodiac)
                             .Include(zodiacHouse => zodiacHouse.House);
        }
    }
}
