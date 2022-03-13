using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.LifeAttributeRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/lifeattributes")]
    [ApiController]
    public class LifeAttributeController : ControllerBase
    {
        private IUnitOfWork _work;
        private ILifeAttributeService lifeAttributeService;
        public LifeAttributeController(IUnitOfWork _work, ILifeAttributeService lifeAttributeService)
        {
            this._work = _work;
            this.lifeAttributeService = lifeAttributeService;
        }

        [HttpGet("{id}")]
        public IActionResult GetLifeAttribute(int id)
        {
            try
            {
                return Ok(lifeAttributeService.GetLifeAttribute(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }

        }

        [HttpGet]
        public IActionResult FindLifeAttribute(int id, string name, string sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                int total = 0;
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize
                };

                FindLifeAttributeRequest request = new FindLifeAttributeRequest()
                {
                    Id = id,
                    Name = name,
                    PagingRequest = pagingRequest,
                };
                var result = lifeAttributeService.FindLifeAttribute(request, out total);
                PagingView pagingView = new PagingView()
                {
                    Payload = result,
                    Total = total,
                };
                return Ok(pagingView);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateLifeAttribute(CreateLifeAttributeRequest request)
        {
            try
            {
                return Ok(lifeAttributeService.CreateLifeAttribute(request));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ReomoveLifeAttribute(int id)
        {
            try
            {
                return Ok(lifeAttributeService.DeleteLifeAttribute(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public IActionResult UpdateZodiac(int id, UpdateLifeAttributeRequest request)
        {
            try
            {
                return Ok(lifeAttributeService.UpdateLifeAttribute(id, request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
    }
}
