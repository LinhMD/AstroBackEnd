using AstroBackEnd.Models;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Repositories.Core
{
    public interface IZodiacHouseRepository : IRepository<ZodiacHouse>
    {
        public ZodiacHouse GetZodiacHouseWithAllData(int id);

        public IEnumerable<ZodiacHouse> FindPPagingZodiacHouseWithAllData<TSortBy>(Func<ZodiacHouse, bool> filter, Func<ZodiacHouse, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20);

        public IEnumerable<ZodiacHouse> FindZodiacHouseWithAllData<TSortBy>(Func<ZodiacHouse, bool> filter, Func<ZodiacHouse, TSortBy> sortBy, out int total);
    }
}
