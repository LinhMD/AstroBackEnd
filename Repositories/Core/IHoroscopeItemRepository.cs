using AstroBackEnd.Models;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Repositories.Core
{
    public interface IHoroscopeItemRepository : IRepository<HoroscopeItem>
    {
        public HoroscopeItem GetHoroscopeItemWithAllData(int id);

        public IEnumerable<HoroscopeItem> FindHoroscopeItemWithAllData<TSortBy>(Func<HoroscopeItem, bool> filter, Func<HoroscopeItem, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20);

        public IEnumerable<HoroscopeItem> FindHoroscopeItemWithAllData<TSortBy>(Func<HoroscopeItem, bool> filter, Func<HoroscopeItem, TSortBy> sortBy, out int total);
    }
}
