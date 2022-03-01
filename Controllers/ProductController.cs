
using AstroBackEnd.Services.Core;
using AstroBackEnd.RequestModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using AstroBackEnd.Repositories;
using AstroBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using AstroBackEnd.RequestModels.ProductRequest;
using AstroBackEnd.ViewsModel;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _Service;
        private IUnitOfWork _work;

        public ProductController(IProductService service, IUnitOfWork work)
        {
            this._Service = service;
            this._work = work;
        }
        [HttpGet]
        public IActionResult GetAllProduct()
        {
            Func<Product, ViewsModel.ProductView> maping = product =>
            {
                return new ProductView()
                {
                    Id = product.Id,
                    //MasterProductId = product.MasterProduct.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Detail = product.Detail,
                    //CatagoryId = product.Catagory.Id,
                    Size = product.Size,
                    Price = product.Price,
                    Gender = product.Gender,
                    Color = product.Color,
                    Inventory = product.Inventory
                };

            };
            return Ok(_Service.GetAllProduct().Select(maping));
        }

        [HttpGet("{id}")]
        public IActionResult GetProduct(int id)
        {
            var product = _Service.GetProduct(id);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductsCreateRequest request)
        {
            return Ok(_Service.CreateProduct(request));
        }

        [HttpPost]
        [Route("CreateMasterProduct")]
        public IActionResult CreateMasterProduct([FromBody] MasterProductCreateRequest request)
        {
            var result = _Service.CreateMasterProduct(request);
            if (result == null)
            {
                return BadRequest(new { StatusCodes = 404, Message = "Can't create product" });
            }
            else
            {
                return Ok(new { StatusCode = 200, message = "The request has been completed successfully", data = result });
            }
        }

        [HttpGet]
        [Route("Product")]
        public IActionResult FindProduct(int id, string name,
            string description, int catagoryId,
            string size, double price,
            int gender, string color,
            string sortBy, int page, int pageSize)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindProductsRequest findProductsRequest = new FindProductsRequest()
                {
                    Id = id,
                    Name = name,
                    Description= description,
                    CatagoryId=catagoryId,
                    Size=size,
                    Price=price,
                    Gender=gender,
                    Color=color
                };
                return Ok(_Service.FindProducts(findProductsRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, ProductsUpdateRequest request)
        {
            Product updateProduct = _Service.UpdateProduct(id, request);

            return Ok(updateProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            //_Service.DeleteCatagory(id);
            //return Ok();
            Product product = _work.Product.Get(id);
            if (product != null)
            {
                _Service.DeleteProduct(id);
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
