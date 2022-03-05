
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
using AstroBackEnd.RequestModels.CategoryRequest;
using AstroBackEnd.ViewsModel;
using System.ComponentModel.DataAnnotations;
using AstroBackEnd.Utilities;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategorysService _Service;
        private IUnitOfWork _work;
        public CategoryController(ICategorysService service, IUnitOfWork work)
        {
            this._Service = service;
            this._work = work;
        }

        
        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
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
        public IActionResult FindCategory(int? id, string name, string sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindCategoryRequest findCategoryRequest = new FindCategoryRequest()
                {
                    Id=id,
                    Name = name
                };
                return Ok(_Service.FindCategory(findCategoryRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateCategory(int id, CategoryUpdateRequest request)
        {
            Category updateCategory = _Service.UpdateCategory(id, request);

            return Ok(updateCategory);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            Category category = _work.Categorys.Get(id);
            if (category!=null)
            {
                _Service.DeleteCategory(id);
                return Ok(category);
            }
            else
            {
                return NotFound();
            }
        }
    }   
}
