
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
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ICategoriesService _Service;
        private IUnitOfWork _work;
        public CategoryController(ICategoriesService service, IUnitOfWork work)
        {
            this._Service = service;
            this._work = work;
        }


        [HttpGet("{id}")]
        public IActionResult GetCategory(int id)
        {
            try
            {
                return Ok(_Service.GetCategory(id));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message); 
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateCategory([FromBody] CategoryCreateRequest request)
        {
            try
            {
                Validation.Validate(request);
                return Ok(_Service.CreateCategory(request));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
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
                    Id = id,
                    Name = name
                };
                int total = 0;
                IEnumerable<Category> categories = _Service.FindCategory(findCategoryRequest, out total);
                return Ok(new PagingView()
                {
                    Payload = categories,
                    Total = total
                });
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateCategory(int id, CategoryUpdateRequest request)
        {
            try
            {
                Category updateCategory = _Service.UpdateCategory(id, request);

                return Ok(updateCategory);
            }
            catch (ArgumentException ex) 
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message); 
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                return Ok(_Service.DeleteCategory(id));
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
