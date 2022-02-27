using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetZodiacRequest;
using AstroBackEnd.Services.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/planetzodiac")]
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

        [HttpGet("id")]
        public IActionResult GetPlanetZodiac(int id)
        {
            PlanetZodiac planetZodiac = planetZodiacService.GetPlanetZodiac(id);
            if (planetZodiac == null)
                return NotFound();
            else
                return Ok(planetZodiac);
        }

        [HttpGet]
        public IActionResult FindPlanetZodiac(int id, string planetId, string zodiacId, string content, string sortBy, int page, int pageSize)
        {
            try
            {
                int checkplanetId = 0;
                int checkzodiacId = 0;
                if (!string.IsNullOrWhiteSpace(planetId))
                {
                    checkplanetId = Int32.Parse(planetId);
                }
                if (!string.IsNullOrWhiteSpace(zodiacId))
                {
                    checkzodiacId = Int32.Parse(zodiacId);
                }
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };

                FindPlanetZodiacRequest findPlanetZodiacRequest = new FindPlanetZodiacRequest()
                {
                    Id = id,
                    Content = content,
                    PlanetId = checkplanetId,
                    ZodiacId = checkzodiacId,
                    PagingRequest = pagingRequest
                };
                return Ok(planetZodiacService.FindPlanetZodiac(findPlanetZodiacRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
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

        [HttpDelete]
        public IActionResult DeletePlanetZodiac(int id)
        {
            PlanetZodiac planetZodiac = planetZodiacService.DeletePlanetZodiac(id);
            if (planetZodiac == null)
                return NotFound();
            else
                return Ok(planetZodiac);

        }

        [HttpPut]
        public IActionResult UpdatePlanet(int id, UpdatePlanetZodiacRequest request)
        {
            PlanetZodiac planetZodiac = planetZodiacService.UpdatePlanetZodiac(id, request);
            if (planetZodiac == null)
            {
                return NotFound();
            }
            return Ok(planetZodiac);
        }
    }
}
