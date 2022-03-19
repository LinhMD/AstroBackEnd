using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using System.Linq;

namespace AstroBackEnd.Repositories.Implement
{
    public class HoroscopeRepository : Repository<Horoscope>, IHoroscopeRepository
    {
        public HoroscopeRepository( AstroDataContext dataContext) : base(dataContext)
        {

        }
        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public override IQueryable<Horoscope> WithAllData()
        {
            throw new System.NotImplementedException();
        }
    }
}
