using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories.Core
{
    public interface IZodiacProductRepositorye : IRepository<ProductZodiac>
    {
        public ProductZodiac GetAllProductZodiac(int id);
    }
}
