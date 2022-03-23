using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using System.Linq;

namespace AstroBackEnd.Repositories.Implement
{
    public class PlanetRepository : Repository<Planet>, IPlanetRepository
    {
        public PlanetRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }

        private AstroDataContext AstroDataContext { get { return base._context as AstroDataContext; } }

        public override IQueryable<Planet> WithAllData()
        {
            return AstroDataContext.Planets.AsQueryable();
        }
    }
}
