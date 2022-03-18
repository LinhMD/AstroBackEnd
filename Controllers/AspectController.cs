using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.AspectRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/aspects")]
    [ApiController]
    public class AspectController : ControllerBase
    {
        private readonly IUnitOfWork _work;
        private IAspectService aspectService;
        public AspectController(IUnitOfWork _work, IAspectService aspectService)
        {
            this._work = _work;
            this.aspectService = aspectService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateAspect([FromBody] CreateAspectRequest request)
        {
            try
            {
                return Ok(new AspectView(aspectService.CreateAspect(request)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetAspect(int id)
        {
            try
            {
                Aspect aspect = aspectService.GetAspect(id);
                var aspectView = new AspectView(aspect);
                return Ok(aspectView);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult FindAspect(int id, int planetBaseId, int planetCompareId, string sortBy, int angleType = -1,  int page = 1, int pageSize = 20)
        {
            try
            {
                int total = 0;
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindAspectRequest request = new FindAspectRequest()
                {
                    Id = id,
                    PlanetBaseId = planetBaseId,
                    PlanetCompareId = planetCompareId,
                    AngleType = angleType,
                    PagingRequest = pagingRequest
                };
                var findResult = aspectService.FindAspect(request, out total).Select(aspect => new AspectSimpleView(aspect));
                PagingView pagingView = new PagingView()
                {
                    Payload = findResult,
                    Total = total
                };
                return Ok(pagingView);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateAspect(int id, [FromBody] UpdateAspectRequest request)
        {
            try
            {
                return Ok(new AspectView(aspectService.UpdateAspect(id, request)));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteAspect(int id)
        {
            try
            {
                Response.Headers.Add("Allow", "GET, POST, PUT");
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
                /*return Ok(aspectService.DeleteAspect(id));*/
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet("calculate")]
        [Authorize(Roles = "admin")]
        public IActionResult CalulateAspect(DateTime birthDate, DateTime compareDate)
        {

            return Ok(aspectService.CalculateAspect(birthDate, compareDate));
        } 
    }
}
