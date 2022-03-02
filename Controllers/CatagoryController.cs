
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
using AstroBackEnd.RequestModels.CatagoryRequest;
using AstroBackEnd.ViewsModel;
using System.ComponentModel.DataAnnotations;
using AstroBackEnd.Utilities;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/catagory")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        private ICategorysService _Service;
        private IUnitOfWork _work;
        public CatagoryController(ICategorysService service, IUnitOfWork work)
        {
            this._Service = service;
            this._work = work;
        }

        
        [HttpGet("{id}")]
        public IActionResult GetCatagory(int id)
        {
            var product = _Service.GetCategory(id);
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
        public IActionResult CreateProduct([FromBody] CategoryCreateRequest request)
        {
            try
            {
                Validation.Validate(request);
                return Ok(_Service.CreateCategory(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet]
        //[Route("catagory")]
        public IActionResult FindCatagory(int id, string name, string sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindCategoryRequest findCatagoryRequest = new FindCategoryRequest()
                {
                    Id=id,
                    Name = name
                };
                return Ok(_Service.FindCategory(findCatagoryRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateProduct(int id, CatagoryUpdateRequest request)
        {
            Category updateCatagory = _Service.UpdateCategory(id, request);

            return Ok(updateCatagory);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCatagory(int id)
        {
            //_Service.DeleteCatagory(id);
            //return Ok();
            Category catagory = _work.Categorys.Get(id);
            if (catagory!=null)
            {
                _Service.DeleteCategory(id);
                return Ok(catagory);
            }
            else
            {
                return NotFound();
            }
        }

    }

    
}
