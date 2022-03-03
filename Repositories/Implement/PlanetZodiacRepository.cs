using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;

namespace AstroBackEnd.Repositories.Implement
{
    public class PlanetZodiacRepository : Repository<PlanetZodiac>, IPlanetZodiacRepository
    {
        public PlanetZodiacRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }
        private AstroDataContext AstroDataContext { get { return base._context as AstroDataContext; } }
    }
}
