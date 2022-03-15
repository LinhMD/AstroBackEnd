using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.AspectRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
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
        public IActionResult CreateAspect([FromBody] CreateAspectRequest request)
        {
            try
            {
                return Ok(aspectService.CreateAspect(request));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
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
        public IActionResult FindAspect(int id, int planetBaseId, int planetCompareId, int angleType, string sortBy, int page = 1, int pageSize = 20)
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
                var findResult = aspectService.FindAspect(request, out total).Select(aspect => new AspectView(aspect));
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
        public IActionResult UpdateAspect(int id, [FromBody] UpdateAspectRequest request)
        {
            try
            {
                return Ok(aspectService.UpdateAspect(id, request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAspect(int id)
        {
            try
            {
                return Ok(aspectService.DeleteAspect(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
    }
}
