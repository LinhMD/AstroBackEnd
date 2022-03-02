using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Services.Core;
using AstroBackEnd.RequestModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstroBackEnd.RequestModels.ProductRequest;
using AstroBackEnd.RequestModels.ZodiacProductRequest;

namespace AstroBackEnd.Services.Implement
{
    public class ZodiacProductService : IZodiacProductService, IDisposable
    {
        private readonly IUnitOfWork _work;

        private readonly AstroDataContext _astroData;
        public ZodiacProductService(IUnitOfWork work, AstroDataContext astroData)
        {
            this._work = work;
            this._astroData = astroData;
        }

        public ProductZodiac CreateProductZodiac(ZodiacProductsCreateRequest request)
        {
            var pro = _work.Product.Get(request.ProductId);
            var zodiac = _work.Zodiac.Get(request.ZodiacId);
            ProductZodiac productZodiac = new ProductZodiac()
            {
                ProductId = request.ProductId,
                Product = pro,
                ZodiacId = request.ZodiacId,
                Zodiac = zodiac
            };
            return _work.ZodiacProduct.Add(productZodiac);
        }

        public ProductZodiac DeleteProductZodiac(int id)
        {
            ProductZodiac productZodiac = _work.ZodiacProduct.Get(id);
            if (productZodiac != null)
            {
                _work.ZodiacProduct.Remove(GetProductZodiac(id));
                return productZodiac;
            }
            else
            {
                return null;
            }
        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public IEnumerable<ProductZodiac> FindProductZodiac(FindZodiacProductRequest request)
        {
            Func<ProductZodiac, bool> filter = p =>
            {
                bool checkId = true;
                checkId = request.Id == null ? true : p.Id == request.Id;

                bool checkProductId = true;
                checkProductId = request.ProductId == null ? true : p.ProductId == request.ProductId;

                bool checkZodiacId = true;
                checkZodiacId = request.ZodiacId == null ? true : p.ZodiacId == request.ZodiacId;


                return checkId && checkProductId && checkZodiacId ;
            };
            IEnumerable<ProductZodiac> result = null;

            if (request.PagingRequest != null)
            {
                switch (request.PagingRequest.SortBy)
                {
                    case "ProductId":
                        result = _work.ZodiacProduct.FindPaging(filter, p => p.ProductId, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "ZodiacId":
                        result = _work.ZodiacProduct.FindPaging(filter, p => p.ZodiacId, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;

                    default:
                        result = _work.ZodiacProduct.FindPaging(filter, p => p.ProductId, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                }
            }
            else
            {
                result = _work.ZodiacProduct.Find(filter, p => p.ProductId);
            }

            return result;
        }

        public IEnumerable<ProductZodiac> GetAllProductZodiac()
        {
            return _work.ZodiacProduct.GetAll(p => p.ProductId);
        }

        public ProductZodiac GetProductZodiac(int id)
        {
            return _work.ZodiacProduct.Get(id);
        }

        public ProductZodiac UpdateProductZodiac(int id, ZodiacProductsUpdateRequest request)
        {
            var ProductZodiac = _work.ZodiacProduct.Get(id);
            var pro = _work.Product.Get(request.ProductId);
            var zodiac = _work.Zodiac.Get(request.ZodiacId);
            if (!string.IsNullOrWhiteSpace(Convert.ToString(request.ProductId)))
            {
                ProductZodiac.ProductId = request.ProductId;
            }
            
            if (!string.IsNullOrWhiteSpace(Convert.ToString(request.ZodiacId)))
            {
                ProductZodiac.ZodiacId = request.ZodiacId;
            }
            return ProductZodiac;
        }
    }    
}
