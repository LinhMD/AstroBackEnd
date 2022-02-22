using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;

namespace AstroBackEnd.Repositories.Implement
{
    public class ZodiacRepository : Repository<Zodiac>, IZodiacRepository
    {
        public ZodiacRepository(Data.AstroDataContext dataContext) : base(dataContext)
        {

        }

        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }
    }
}
