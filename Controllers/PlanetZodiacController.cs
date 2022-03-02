using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetZodiacRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
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
            if (planetZodiac != null)
                return Ok(planetZodiac);
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult FindPlanetZodiac(int id, int planetId, int zodiacId, string content, string sortBy, int page = 1, int pageSize = 20)
        {
            try
            { 
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
                    PlanetId = planetId,
                    ZodiacId = zodiacId,
                    PagingRequest = pagingRequest
                };

                Validation.Validate(findPlanetZodiacRequest);

                return Ok(planetZodiacService.FindPlanetZodiac(findPlanetZodiacRequest));
            }
            catch (ArgumentException ex)
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

        [HttpDelete("{id}")]
        public IActionResult DeletePlanetZodiac(int id)
        {
            PlanetZodiac planetZodiac = planetZodiacService.DeletePlanetZodiac(id);
            if (planetZodiac != null)
                return Ok(planetZodiac);
            else
                return NotFound();
        }

        [HttpPut]
        public IActionResult UpdatePlanet(int id, UpdatePlanetZodiacRequest request)
        {
            PlanetZodiac planetZodiac = planetZodiacService.UpdatePlanetZodiac(id, request);
            if (planetZodiac != null)
            {
                return Ok(planetZodiac);
            }
            return NotFound();  
        }
    }
}
