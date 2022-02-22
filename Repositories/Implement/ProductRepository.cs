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
    public class ProductRepository : Repository<Product> , IProductRepository
    {
        public ProductRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }
        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public Product GetAllProductData(int id)
        {
            return AstroData.Products.Include("MasterProduct")
                .Include("Catagory").Include("ImgLinks").Include("Zodiacs")
                .Include("ProductVariation").First(p => p.Id == id);
        }
    }
}
