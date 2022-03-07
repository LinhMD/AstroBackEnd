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

        public Product GetMasterProduct(int id);

        public Product GetProductVariant(int id);

        public IEnumerable<Product> GetAllProduct();

        public IEnumerable<Product> FindProductVariant(FindProductsVariantRequest Request);

        public IEnumerable<Product> FindProductVariant(FindProductsVariantRequest Request, out int total);

        public IEnumerable<Product> FindMasterProduct(FindMasterProductRequest request);

        public IEnumerable<Product> FindMasterProduct(FindMasterProductRequest request, out int total);

        public void DeleteProduct(int id);

        public Product UpdateMasterProduct(int id, MasterProductsUpdateRequest request);

        public Product UpdateProductVariant(int id, ProductVariantUpdateRequest request);


        public Product CreateProductVariant(ProductVariantCreateRequest Product);

        public Product CreateMasterProduct(MasterProductCreateRequest Product);

        
    }
}
