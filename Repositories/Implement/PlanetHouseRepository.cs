
using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Repositories.Implement
{
    public class PlanetHouseRepository : Repository<PlanetHouse>, IPlanetHouseRepository
    {
        public PlanetHouseRepository(AstroDataContext dataContext) : base(dataContext)
        {
        }
        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public IEnumerable<PlanetHouse> FindPlanetHouseWithAllData<TSortBy>(Func<PlanetHouse, bool> filter, Func<PlanetHouse, TSortBy> sortBy, out int total)
        {
            var planetHouse = AstroData.PlanetHouses
                 .Include(planetHouse => planetHouse.Planet)
                 .Include(planetHouse => planetHouse.House)
                 .Where(filter)
                 .OrderBy(sortBy);
            total = planetHouse.Count();
            return planetHouse;
                 
        }

        public IEnumerable<PlanetHouse> FindPPaginglanetHouseWithAllData<TSortBy>(Func<PlanetHouse, bool> filter, Func<PlanetHouse, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20)
        {
            var planetHouse = AstroData.PlanetHouses
                 .Include(planetHouse => planetHouse.Planet)
                 .Include(planetHouse => planetHouse.House)
                 .Where(filter)
                 .OrderBy(sortBy);
            total = planetHouse.Count();
            return planetHouse.Skip((page - 1) * pageSize).Take(pageSize + 1);
        }

        public PlanetHouse GetPlanetHouseWithAllData(int id)
        {
            return AstroData.PlanetHouses
                .Include(planetHouse => planetHouse.Planet)
                .Include(planetHouse => planetHouse.House)
                .FirstOrDefault(planetHouse => planetHouse.Id == id);
        }
    }
}
