
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
using AstroBackEnd.RequestModels.ZodiacProductRequest;
using AstroBackEnd.ViewsModel;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/zodiacProduct")]
    [ApiController]
    public class ZodiacProductController : ControllerBase
    {
        private IZodiacProductService _Service;
        private IUnitOfWork _work;

        public ZodiacProductController(IZodiacProductService service, IUnitOfWork work)
        {
            this._Service = service;
            this._work = work;
        }

        [HttpGet]
        public IActionResult GetAllZodiacProduct()
        {
            Func<ProductZodiac, ViewsModel.ZodiacProductView> maping = zodiacProduct =>
            {
                return new ZodiacProductView()
                {
                    Id = zodiacProduct.Id,
                    ProductId=zodiacProduct.ProductId,
                    ZodiacId=zodiacProduct.ZodiacId
                };

            };
            return Ok(_Service.GetAllProductZodiac().Select(maping));
        }

        [HttpGet("{id}")]
        public IActionResult GetZodiacProductt(int id)
        {
            var ZodiacProduct = _Service.GetProductZodiac(id);
            if (ZodiacProduct != null)
            {
                return Ok(ZodiacProduct);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateZodiacProduct([FromBody] ZodiacProductsCreateRequest request)
        {
            return Ok(_Service.CreateProductZodiac(request));
        }

        [HttpGet]
        [Route("ZodiacProduct")]
        public IActionResult FindZodiacProduct(int id, int productId, int zodiacId, string sortBy, int page, int pageSize)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindZodiacProductRequest findZodiacProductRequest = new FindZodiacProductRequest()
                {
                    Id = id,
                    ProductId = productId,
                    ZodiacId = zodiacId
                };
                return Ok(_Service.FindProductZodiac(findZodiacProductRequest));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateZodiacProduct(int id, ZodiacProductsUpdateRequest request)
        {
            ProductZodiac updateZodiacProduct = _Service.UpdateProductZodiac(id, request);

            return Ok(updateZodiacProduct);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteZodiacProduct(int id)
        {
            //_Service.DeleteCatagory(id);
            //return Ok();
            ProductZodiac productZodiac = _work.ZodiacProduct.Get(id);
            if (productZodiac != null)
            {
                _Service.DeleteProductZodiac(id);
                return Ok(productZodiac);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
