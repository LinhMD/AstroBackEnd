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
                .Include("Category").Include("ImgLinks").Include("Zodiacs")
                .Include("ProductVariation").FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> FindProducWithAllData<TSortBy>(Func<Product, bool> filter, Func<Product, TSortBy> sortBy, int page, int pageSize)
        {
            return AstroData.Products.Include("MasterProduct")
                                        .Include("Category")
                                        .Include("ImgLinks")
                                        .Include("Zodiacs")
                                        .Include("ProductVariation")
                                        .Where(filter)
                                        .OrderBy(sortBy)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize);
        }

        public IEnumerable<Product> FindProductMasterWithAllData<TSortBy>(Func<Product, bool> filter, Func<Product, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20)
        {
            var products = AstroData.Products
                                        .Include("MasterProduct")
                                        .Include("Category")
                                        .Include("ImgLinks")
                                        .Include("Zodiacs")
                                        .Include("ProductVariation")
                                        .Where(filter)
                                        .OrderBy(sortBy);

            total = products.Count();

            return products
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
    }
}
