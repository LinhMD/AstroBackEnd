using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories.Implement
{
    public class ZodiacRepository : Repository<Zodiac> ,IZodiacRepository
    {
        public ZodiacRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }

        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public override IQueryable<Zodiac> WithAllData()
        {
            return AstroData.Zodiacs.AsQueryable().Include(z => z.Quotes);
        }
    }
}
