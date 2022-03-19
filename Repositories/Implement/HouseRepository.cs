using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AstroBackEnd.Repositories.Implement
{

    public class HouseRepository : Repository<House>, IHouseRepository
    {
        public HouseRepository(Data.AstroDataContext dataContext) : base(dataContext)
        {

        }

        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public override IQueryable<House> WithAllData()
        {
            return AstroData.Houses.AsQueryable().Include(h => h.Topics);
        }
    }
}
