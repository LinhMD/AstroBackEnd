using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Repositories.Implement
{
    public class PlanetZodiacRepository : Repository<PlanetZodiac>, IPlanetZodiacRepository
    {
        public PlanetZodiacRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }
        private AstroDataContext AstroDataContext { get { return base._context as AstroDataContext; } }

        public PlanetZodiac GetPlanetZodiacWithAllData(int id)
        {
            return AstroDataContext.PlanetZodiacs
                .Include(planetZodiac => planetZodiac.Planet)
                .Include(planetZodiac => planetZodiac.Zodiac)
                .FirstOrDefault(p => p.Id == id) ;
        }

        public IEnumerable<PlanetZodiac> FindPlanetZodiacWithAllData<TSortBy>(Func<PlanetZodiac, bool> filter, Func<PlanetZodiac, TSortBy> sortBy, out int total)
        {
            var planeZodiacs = AstroDataContext.PlanetZodiacs
                .Include(planetZodiac => planetZodiac.Planet)
                .Include(planetZodiac => planetZodiac.Zodiac)
                                        .Where(filter)
                                        .OrderBy(sortBy);

            total = planeZodiacs.Count();
            return planeZodiacs;
        }

        public IEnumerable<PlanetZodiac> FindPPaginglanetZodiacWithAllData<TSortBy>(Func<PlanetZodiac, bool> filter, Func<PlanetZodiac, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20)
        {
            var planeZodiacs = AstroDataContext.PlanetZodiacs
                .Include(planetZodiac => planetZodiac.Planet)
                .Include(planetZodiac => planetZodiac.Zodiac)
                                        .Where(filter)
                                        .OrderBy(sortBy);
                                        
            total = planeZodiacs.Count();
            return planeZodiacs.Skip((page - 1) * pageSize).Take(pageSize + 1);
        }
    }
}
