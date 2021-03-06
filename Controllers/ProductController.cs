
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
    [Route("api/v1/products")]
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteProduct(int id)
        {
            _Service.DeleteProduct(id);
            return Ok();
            /*Response.Headers.Add("Allow", "GET, POST, PUT");
            return StatusCode(StatusCodes.Status405MethodNotAllowed);*/
        }

        [HttpGet("master/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetProduct(int id)
        {
            var product = _Service.GetMasterProduct(id);
            return product == null? NotFound("Master Product id {" +id + "} not found!!") : Ok(new MasterProductView(product));
        }

        
        [HttpGet("master")]
        public IActionResult GetAllProductMaster(int? id, string? name, string? description, string? tag,string? detail, int? categoryId, int? zodiacsId, int? productVariationId, int? status, string? sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                int total = 0;
                IEnumerable<MasterProductView> products = _Service.FindMasterProduct(new FindMasterProductRequest()
                {
                    Id = id,
                    CategoryId = categoryId,
                    Description = description,
                    Detail = detail,
                    Tag = tag,
                    Name = name,
                    ProductVariationId = productVariationId,
                    Status = status,
                    PagingRequest = new PagingRequest() { Page = page, PageSize = pageSize, SortBy = sortBy }

                }, out total).Select(p => new MasterProductView(p));

                return Ok(new PagingView() { Payload = products, Total = total });
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("master")]
        [Authorize(Roles = "admin")]
        public IActionResult CreateMasterProduct([FromBody] MasterProductCreateRequest request)
        {
            try
            {
                return Ok(new MasterProductView(_Service.CreateMasterProduct(request)));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("master")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateProduct(int id, MasterProductsUpdateRequest request)
        {
            try
            {
                Product updateProduct = _Service.UpdateMasterProduct(id, request);

                return Ok(new MasterProductView(updateProduct));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("variant/{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetMasterProduct(int id)
        {
            var product = _Service.GetProductVariant(id);
            return product == null ? NotFound("Product variant id {" + id + "} not found") : Ok(new ProductVariationView(product));
        }

        [HttpGet("variant")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAllProductVariant(string? Size, double? Price, int? Gender, string? Color, int? status, string? SortBy, int Page = 1, int pageSize = 20)
        {
            try
            {
                int total = 0;

                var products = _Service.FindProductVariant(new FindProductsVariantRequest() { 
                    Color = Color,
                    Gender = Gender,
                    Price = Price,
                    Size = Size,
                    Status = status,
                    PagingRequest =  new PagingRequest() { Page = Page, PageSize = pageSize, SortBy = SortBy }
                }, out total).Select(p => new ProductVariationView(p));

                PagingView pagingView = new PagingView()
                {
                    Payload = products,
                    Total = total
                };

                return Ok(pagingView);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("variant")]
        [Authorize(Roles = "admin")]
        public IActionResult CreateProduct([FromBody] ProductVariantCreateRequest request)
        {
            try
            {

                return Ok(new ProductVariationView(_Service.CreateProductVariant(request)));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("variant")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateProductVariant(int id, ProductVariantUpdateRequest request)
        {
            try
            {
                Product updateProduct = _Service.UpdateProductVariant(id, request);
                return Ok(new ProductVariationView(updateProduct));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}
