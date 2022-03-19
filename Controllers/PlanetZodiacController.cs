using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetZodiacRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/planets/{planetName}-{planetId}/zodiacs")]
    [ApiController]
    public class PlanetZodiacController : ControllerBase
    {
        private readonly IPlanetZodiacService planetZodiacService;
        private readonly IUnitOfWork _work;

        public PlanetZodiacController(IPlanetZodiacService planetZodiacService, IUnitOfWork _work)
        {
            this.planetZodiacService = planetZodiacService;
            this._work = _work;
        }

        [HttpGet("{id}")]
        public IActionResult GetPlanetZodiac(int id)
        {
            try
            {
                var planetZodiac = planetZodiacService.GetPlanetZodiac(id);
               
                return Ok(new PlanetZodiacView(planetZodiac));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        public IActionResult FindPlanetZodiac(int id, int planetId, int zodiacId, string sortBy, int page = 1, int pageSize = 20)
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

                FindPlanetZodiacRequest findPlanetZodiacRequest = new FindPlanetZodiacRequest()
                {
                    Id = id,
                    PlanetId = planetId,
                    ZodiacId = zodiacId,
                    PagingRequest = pagingRequest
                };

                Validation.Validate(findPlanetZodiacRequest);
                var findResult = planetZodiacService.FindPlanetZodiac(findPlanetZodiacRequest, out total).Select(pl => new PlanetZodiacView(pl));
                PagingView pagingView = new PagingView()
                {
                    Payload = findResult,
                    Total = total
                };
                return Ok(pagingView);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreatePlanetZodiac(CreatePlanetZodiacRequest request)
        {
            try
            {
                return Ok(planetZodiacService.CreatePlanetZodiac(request));
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public IActionResult DeletePlanetZodiac(int id)
        {
            try
            {
                Response.Headers.Add("Allow", "GET, POST, PUT");
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
                /*return Ok(planetZodiacService.DeletePlanetZodiac(id));*/
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult UpdatePlanet(int id, UpdatePlanetZodiacRequest request)
        {
            try
            {
                return Ok(planetZodiacService.UpdatePlanetZodiac(id, request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
    }
}
