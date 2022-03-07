﻿
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

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                return Ok(_Service.DeleteProduct(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("master/{id}")]
        public IActionResult GetProduct(int id)
        {
            //var product = _Service.GetMasterProduct(id);
            //return product == null? NotFound("Master Product id {" +id + "} not found!!") : Ok(new MasterProductView(product));
            try
            {
                return Ok(_Service.GetMasterProduct(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        
        [HttpGet("master")]
        public IActionResult GetAllProductMaster(int? id, string? name, string? description, string? detail, int? categoryId, int? zodiacsId, int? productVariationId, string? sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                return Ok(_Service.FindMasterProduct(new FindMasterProductRequest() { 
                    Id = id,
                    CategoryId = categoryId,
                    Description = description,
                    Detail = detail,
                    Name = name,
                    ProductVariationId = productVariationId,
                    ZodiacsId = zodiacsId,
                    PagingRequest = new PagingRequest() {  Page = page, PageSize = pageSize , SortBy = sortBy }
                
                }).Select(p => new MasterProductView(p)));
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("master")]
        public IActionResult CreateMasterProduct([FromBody] MasterProductCreateRequest request)
        {
            try
            {
                return Ok(new MasterProductView(_Service.CreateMasterProduct(request)));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPut("master")]
        public IActionResult UpdateProduct(int id, MasterProductsUpdateRequest request)
        {
            try
            {
                Product updateProduct = _Service.UpdateMasterProduct(id, request);

                return Ok(new MasterProductView(updateProduct));
            }
            catch(ArgumentException e)
            {
                
                return BadRequest(e.StackTrace);
            }
        }

        [HttpGet("variant/{id}")]
        public IActionResult GetMasterProduct(int id)
        {
            //var product = _Service.GetProductVariant(id);
            //return product == null ? NotFound("Product variant id {" + id + "} not found!!") : Ok(new ProductVariationView(product));
            try
            {
                return Ok(_Service.GetProductVariant(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("variant")]
        public IActionResult GetAllProductVariant(string? Size, double? Price, int? Gender, string? Color, string? SortBy, int Page = 1, int pageSize = 20)
        {
            try
            {
                return Ok(_Service.FindProductVariant(new FindProductsVariantRequest() { 
                    Color = Color,
                    Gender = Gender,
                    Price = Price,
                    Size = Size,
                    PagingRequest =  new PagingRequest() { Page = Page, PageSize = pageSize, SortBy = SortBy }

                }).Select(p => new ProductVariationView(p)));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("variant")]
        public IActionResult CreateProduct([FromBody] ProductVariantCreateRequest request)
        {
            try
            {
                return Ok(new ProductVariationView(_Service.CreateProductVariant(request)));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("variant")]
        public IActionResult UpdateProductVariant(int id, ProductVariantUpdateRequest request)
        {
            try
            {
                Product updateProduct = _Service.UpdateProductVariant(id, request);
                return Ok(new ProductVariationView(updateProduct));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
