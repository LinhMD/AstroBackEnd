using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstroBackEnd.RequestModels.ProductRequest;
namespace AstroBackEnd.Services.Core
{
    public interface IProductService
    {
        public Product GetProduct(int id);

        public IEnumerable<Product> GetAllProduct();

        public IEnumerable<Product> FindProducts(FindProductsRequest Request);

        public void DeleteProduct(int id);

        public Product UpdateProduct(int id, ProductsUpdateRequest request);

        public Product CreateProduct(ProductsCreateRequest Product);

        public Product CreateMasterProduct(MasterProductCreateRequest Product);
    }
}
