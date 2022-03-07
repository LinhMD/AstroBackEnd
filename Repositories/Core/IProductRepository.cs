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

        public IEnumerable<Product> FindProducWithAllData<TSortBy>(Func<Product, bool> filter, Func<Product, TSortBy> sortBy, int page = 1, int pageSize = 20);

        public IEnumerable<Product> FindProducWithAllData<TSortBy>(Func<Product, bool> filter, Func<Product, TSortBy> sortBy, out int total, int page = 1, int pageSize = 20);

        public IEnumerable<Product> FindProductMasterWithAllData<TSortBy>(Func<Product, bool> filter, Func<Product, TSortBy> sortBy, out int total,  int page = 1, int pageSize = 20);
    }
}
