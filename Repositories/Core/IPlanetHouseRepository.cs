using AstroBackEnd.Models;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Repositories.Core
{
    public interface IPlanetHouseRepository : IRepository<PlanetHouse>
    {
        public PlanetHouse GetPlanetHouseWithAllData(int id);

        public IEnumerable<PlanetHouse> FindPPaginglanetHouseWithAllData<TSortBy>(Func<PlanetHouse, bool> filter, Func<PlanetHouse, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20);

        public IEnumerable<PlanetHouse> FindPlanetHouseWithAllData<TSortBy>(Func<PlanetHouse, bool> filter, Func<PlanetHouse, TSortBy> sortBy, out int total);
    }
}
