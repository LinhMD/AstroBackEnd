using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstroBackEnd.RequestModels.ZodiacProductRequest;

namespace AstroBackEnd.Services.Core
{
    public interface IZodiacProductService
    {
        public ProductZodiac GetProductZodiac(int id);

        public IEnumerable<ProductZodiac> GetAllProductZodiac();

        public IEnumerable<ProductZodiac> FindProductZodiac(FindZodiacProductRequest request);

        public ProductZodiac DeleteProductZodiac(int id);

        public ProductZodiac UpdateProductZodiac(int id, ZodiacProductsUpdateRequest request);

        public ProductZodiac CreateProductZodiac(ZodiacProductsCreateRequest request);

        
    }
}
