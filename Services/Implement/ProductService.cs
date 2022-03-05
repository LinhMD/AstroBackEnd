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

namespace AstroBackEnd.Services.Implement
{
    public class ProductService : IProductService, IDisposable
    {
        private readonly IUnitOfWork _work;

        private readonly AstroDataContext _astroData;
        public ProductService(IUnitOfWork work, AstroDataContext astroData)
        {
            this._work = work;
            this._astroData = astroData;
        }

        public Product CreateMasterProduct(MasterProductCreateRequest request)
        {


            var cata = _work.Categorys.Get(request.CategoryId);
            Product product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Detail = request.Detail,
                Category = cata,
                
            };

            _work.Products.Add(product);
            _work.Image.AddAll(request.ImgLink.Select(i => new ImgLink() { Link = i, ProductId = product.Id }));

            return _work.Products.GetAllProductData(product.Id);

        }

        public Product CreateProductVariant(ProductVariantCreateRequest request)
        {
            Product master = _work.Products.Get(request.MasterProductId);

            Product product = new Product()
            {
                MasterProduct = master,
                Size = request.Size,
                Price = request.Price,
                Gender = request.Gender,
                Color = request.Color,
                Inventory = request.Inventory,
            };

            _work.Products.Add(product);
            _work.Image.AddAll(request.ImgLink.Select(i => new ImgLink() { Link = i, ProductId = product.Id }));

            return _work.Products.GetAllProductData(product.Id);

        }

        public void DeleteProduct(int id)
        {
            _work.Categorys.Remove(_work.Categorys.Get(id));
        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public IEnumerable<Product> FindProductVariant(FindProductsVariantRequest request)
        {
            Func<Product, bool> filter = p =>
            {
                if (p.MasterProduct == null) return false;

                bool checkSize = true;
                if (!string.IsNullOrWhiteSpace(request.Size))
                {
                    checkSize = p.Size.Contains(request.Size);
                }

                bool checkPrice = true;
                checkPrice = request.Price==null || p.Price== request.Price;

                bool checkGender = true;
                checkGender = request.Gender == null || p.Gender == request.Gender;

                bool checkColor = true;
                if (!string.IsNullOrWhiteSpace(request.Color))
                {
                    checkColor = p.Color.Contains(request.Color);
                }
                return checkSize && checkPrice && checkGender && checkColor;
            };

            IEnumerable<Product> result = null;

            if (request.PagingRequest != null)
            {
                switch (request.PagingRequest.SortBy)
                {

                    case "Size":
                        result = _work.Products.FindPaging(filter, p => p.Size, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "Price":
                        result = _work.Products.FindPaging(filter, p => p.Price, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "Gender":
                        result = _work.Products.FindPaging(filter, p => p.Gender, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "Color":
                        result = _work.Products.FindPaging(filter, p => p.Color, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    default:
                        result = _work.Products.FindPaging(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                }
            }
            else
            {
                result = _work.Products.Find(filter, p => p.Name);
            }

            return result;
        }


        public IEnumerable<Product> FindMasterProduct(FindMasterProductRequest request)
        {
            Func<Product, bool> filter = p =>
            {
                if (p.MasterProduct != null) return false;

                bool checkName = string.IsNullOrWhiteSpace(request.Name) || (!string.IsNullOrEmpty(p.Detail) && p.Name.Contains(request.Name)); 

                bool checkDescription = string.IsNullOrWhiteSpace(request.Description) || (!string.IsNullOrEmpty(p.Description) && p.Description.Contains(request.Description));

                bool checkDetail = string.IsNullOrWhiteSpace(request.Detail) || (!string.IsNullOrEmpty(p.Detail) && p.Detail.Contains(request.Detail));

                bool checkCategory = request.CategoryId == null || p.Category.Id == request.CategoryId;

                bool zodiacIdCheck = request.ZodiacsId == null || p.Zodiacs.Select(z => z.Id).Contains(((int)request.ZodiacsId.Value));

                bool variationCheck = request.ProductVariationId == null || p.ProductVariation.Select(z => z.Id).Contains(((int)request.ProductVariationId.Value));

                return checkName && checkDescription && checkCategory;
            };


            IEnumerable<Product> result = null;

            if (request.PagingRequest != null)
            {
                switch (request.PagingRequest.SortBy)
                {
                    case "Name":
                        result = _work.Products.FindProducWithAllData(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "Description":
                        result = _work.Products.FindProducWithAllData(filter, p => p.Description, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "CategoryId":
                        result = _work.Products.FindProducWithAllData(filter, p => p.Category.Id, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    default:
                        result = _work.Products.FindProducWithAllData(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                }
            }
            else
            {
                result = _work.Products.Find(filter, p => p.Name);
            }

            return result;

        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _work.Products.GetAll<String>(p => p.Name);
        }


        public Product GetProduct(int id)
        {

            Product product = _work.Products.GetAllProductData(id);

            if (product == null) throw new ArgumentException("Can not find product with id(" + id + ")");

            return product;

        }

        public Product UpdateMasterProduct(int id, MasterProductsUpdateRequest request)
        {

            var product = this.GetProduct(id);
         

            var cata = _work.Categorys.Get(request.CategoryId);
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                product.Name = request.Name;
            }
            if (!string.IsNullOrWhiteSpace(request.Description))
            {
                product.Description = request.Description;
            }
            if (!string.IsNullOrWhiteSpace(request.Detail))
            {
                product.Detail = request.Detail;
            }
            if (!string.IsNullOrWhiteSpace(Convert.ToString(request.CategoryId)))
            {
                product.Category = cata;
            }
            if(request.ImgLinksAdd != null && request.ImgLinksAdd.Count != 0)
            {
                _work.Image.AddAll(request.ImgLinksAdd.Select(i => new ImgLink() { Link = i, ProductId = id }));
            }

            _work.Complete();

            return this.GetProduct(id);

        }

        public Product UpdateProductVariant(int id, ProductVariantUpdateRequest request)
        {
            var product = this.GetProduct(id);
            

            if (!string.IsNullOrWhiteSpace(request.Size))
            {
                product.Size = request.Size;
            }
            if (request.Price != null && request.Price >= 0D)
            {
                product.Price = request.Price;
            }
            if (request.Gender != null)
            {
                product.Gender = request.Gender;
            }
            if (!string.IsNullOrWhiteSpace(request.Color))
            {
                product.Color = request.Color;
            }
            if(request.Inventory != null && request.Inventory >= 0)
            {
                product.Inventory = request.Inventory;
            }

            if (request.ImgLinksAdd != null && request.ImgLinksAdd.Count != 0)
            {
                IEnumerable<ImgLink> links = request.ImgLinksAdd.Select(i => new ImgLink() { Link = i, ProductId = id });
                _work.Image.AddAll(links);
                foreach (var link in links)
                {
                    product.ImgLinks.Add(link);
                }
            }

            return product;
        }

        public Product GetMasterProduct(int id)
        {
            return _work.Products.FindProducWithAllData(p => p.Id == id && p.MasterProduct == null, p => p.Id).FirstOrDefault();
        }

        public Product GetProductVariant(int id)
        {
            return _work.Products.FindProducWithAllData(p => p.Id == id && p.MasterProduct != null, p => p.Id).FirstOrDefault();
        }

       
    }
}
