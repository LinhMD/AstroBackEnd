using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories.Core
{
    public interface IProductRepository : IRepository<Product>
    {
        public Product GetAllProductData(int id);
    }
}
