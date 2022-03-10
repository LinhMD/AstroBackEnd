using AstroBackEnd.Models;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Repositories.Core
{
    public interface IPlanetZodiacRepository : IRepository<PlanetZodiac>
    {
        public PlanetZodiac GetPlanetZodiacWithAllData(int id);

        public IEnumerable<PlanetZodiac> FindPPaginglanetZodiacWithAllData<TSortBy>(Func<PlanetZodiac, bool> filter, Func<PlanetZodiac, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20);

        public IEnumerable<PlanetZodiac> FindPlanetZodiacWithAllData<TSortBy>(Func<PlanetZodiac, bool> filter, Func<PlanetZodiac, TSortBy> sortBy, out int total);
    }
}
