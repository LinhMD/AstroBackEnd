using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetRequest;
using AstroBackEnd.Services.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/planet")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly IPlanetService planetService;
        private readonly IUnitOfWork _work;

        public PlanetController(IPlanetService planetService, IUnitOfWork _work)
        {
            this.planetService = planetService;
            this._work = _work;
        }

        [HttpGet("id")]
        public IActionResult GetPlanet(int id)
        {
            Planet planet = planetService.GetPlanet(id);
            if (planet == null)
                return NotFound();
            else
                return Ok(planet);
        }

        [HttpGet]
        public IActionResult FindPlanet(int id, string name, string title, string description, string tag, string mainContent, string sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindPlanetRequest request = new FindPlanetRequest()
                {
                    Id = id,
                    Name = name,
                    Title = title,
                    Description = description,
                    Tag = tag,
                    MainContent = mainContent,
                    PagingRequest = pagingRequest,

                };
                return Ok(planetService.FindPlanet(request));
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreatePlanet(CreatePlanetRequest request)
        {
            try
            {
                return Ok(planetService.CreatePlanet(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeletePlanet(int id)
        {
            Planet planet = _work.Planets.Get(id);
            if (planet == null)
                return NotFound();
            else
            {
                _work.Planets.Remove(planet);
                return Ok(planet);
            }
        }

        [HttpPut]
        public IActionResult UpdatePlanet(int id, UpdatePlanetRequest request)
        {
            Planet planet = planetService.UpdatePlanet(id, request);
            if (planet == null)
            {
                return NotFound();
            }
            return Ok(planet);
        }
    }
}
