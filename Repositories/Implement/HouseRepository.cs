using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;

namespace AstroBackEnd.Repositories.Implement
{

    public class HouseRepository : Repository<House>, IHouseRepository
    {
        public HouseRepository(Data.AstroDataContext dataContext) : base(dataContext)
        {

        }

        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }
    }
}
