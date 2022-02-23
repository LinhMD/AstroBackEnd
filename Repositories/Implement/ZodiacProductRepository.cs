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
    public class ZodiacProductRepository : Repository<ProductZodiac>, IZodiacProductRepositorye
    {
        public ZodiacProductRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }

        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public ProductZodiac GetAllProductZodiac(int id)
        {
            return AstroData.ProductZodiacs.First(u => u.Id == id);
        }
    }
}
