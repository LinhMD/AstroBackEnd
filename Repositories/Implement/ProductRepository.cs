using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories.Implement
{
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        public ProductRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }
    }
}
