using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;

namespace AstroBackEnd.Repositories.Implement
{
    public class ZodiacHouseRepository : Repository<ZodiacHouse>, IZodiacHouseRepository
    {
        public ZodiacHouseRepository(Data.AstroDataContext dataContext) : base(dataContext)
        {

        }

        private AstroDataContext AstroData{ get { return base._context as AstroDataContext;  } }
    }
}
