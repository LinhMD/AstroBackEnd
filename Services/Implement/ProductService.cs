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
            var mtId = _work.Product.Get(request.MasterProductId);
            var cata = _work.Catagory.Get(request.CatagoryId);
            Product product = new Product()
            {
                MasterProduct = mtId,
                Name = request.Name,
                Description = request.Description,
                Detail = request.Detail,
                Catagory = cata,
                Size = request.Size,
                Price = request.Price,
                Gender = request.Gender,
                Color = request.Color,
                Inventory = request.Inventory,
            };
            return _work.Product.Add(product);
        }

        public Product CreateProduct(ProductsCreateRequest request)
        {
            var cata = _work.Catagory.Get(request.CatagoryId);
            Product product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Detail = request.Detail,
                Catagory = cata,
                Size = request.Size,
                Price = request.Price,
                Gender = request.Gender,
                Color = request.Color,
                Inventory = request.Inventory,
            };
            
            return _work.Product.Add(product);
        }

        public void DeleteProduct(int id)
        {
            _work.Catagory.Remove(_work.Catagory.Get(id));
        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public IEnumerable<Product> FindProducts(FindProductsRequest request)
        {
            Func<Product, bool> filter = p =>
            {
                bool checkName = true;
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    checkName = p.Name.Contains(request.Name);
                }

                bool checkDescription = true;
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    checkDescription = p.Description.Contains(request.Description);
                }

                bool checkCatagory = request.CatagoryId==null ? true : p.Catagory.Id== request.CatagoryId;

                bool checkSize = true;
                if (!string.IsNullOrWhiteSpace(request.Size))
                {
                    checkSize = p.Size.Contains(request.Size);
                }

                bool checkPrice = true;
                checkPrice = request.Price==null ? true : p.Price== request.Price;

                bool checkGender = true;
                checkGender = request.Gender == null ? true : p.Gender == request.Gender;

                bool checkColor = true;
                if (!string.IsNullOrWhiteSpace(request.Color))
                {
                    checkColor = p.Color.Contains(request.Color);
                }
                return checkName && checkDescription && checkCatagory && checkSize &&
                        checkPrice && checkGender && checkColor;
            };

            IEnumerable<Product> result = null;

            if (request.PagingRequest != null)
            {
                switch (request.PagingRequest.SortBy)
                {
                    case "Name":
                        result = _work.Product.FindPaging(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "Description":
                        result = _work.Product.FindPaging(filter, p => p.Description, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "CatagoryId":
                        result = _work.Product.FindPaging(filter, p => p.Catagory, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "Size":
                        result = _work.Product.FindPaging(filter, p => p.Catagory, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "Price":
                        result = _work.Product.FindPaging(filter, p => p.Catagory, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "Gender":
                        result = _work.Product.FindPaging(filter, p => p.Catagory, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "Color":
                        result = _work.Product.FindPaging(filter, p => p.Catagory, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    default:
                        result = _work.Product.FindPaging(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                }
            }
            else
            {
                result = _work.Product.Find(filter, p => p.Name);
            }

            return result;
        }

        public IEnumerable<Product> GetAllProduct()
        {
            return _work.Product.GetAll<String>(p => p.Name);
        }


        public Product GetProduct(int id)
        {
            return _work.Product.Get(id);
        }

        public Product UpdateProduct(int id, ProductsUpdateRequest request)
        {
            var product = _work.Product.Get(id);
            var mtId = _work.Product.Get(request.MasterProductId);
            var cata = _work.Catagory.Get(request.CatagoryId);
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
            if (!string.IsNullOrWhiteSpace(Convert.ToString(request.CatagoryId)))
            {
                product.Catagory = cata;
            }
            if (!string.IsNullOrWhiteSpace(request.Size))
            {
                product.Size = request.Size;
            }
            if (!string.IsNullOrWhiteSpace(Convert.ToString(request.Price)))
            {
                product.Price = request.Price;
            }
            if (!string.IsNullOrWhiteSpace(Convert.ToString(request.Gender)))
            {
                product.Gender = request.Gender;
            }
            if (!string.IsNullOrWhiteSpace(request.Color))
            {
                product.Color = request.Color;
            }
            if (!string.IsNullOrWhiteSpace(Convert.ToString(request.Inventory)))
            {
                product.Inventory = request.Inventory;
            }
            return product;

        }
    }
}
