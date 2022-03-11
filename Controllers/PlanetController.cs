using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/planets")]
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

        [HttpGet("{id}")]
        public IActionResult GetPlanet(int id)
        {
            try
            {
                return Ok(planetService.GetPlanet(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        public IActionResult FindPlanet(int id, string name, string title, string icon, string tag, string sortBy, int page = 1, int pageSize = 20)
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
                FindPlanetRequest request = new FindPlanetRequest()
                {
                    Id = id,
                    Name = name,
                    Title = title,
                    Tag = tag,
                    PagingRequest = pagingRequest,
                };
                var findResult = planetService.FindPlanet(request, out total).Select(planet => new PlanetView(planet));
                PagingView pagingView = new PagingView()
                {
                    Payload = findResult,
                    Total = total
                };
                return Ok(pagingView);
            }catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public IActionResult CreatePlanet(CreatePlanetRequest request)
        {
            try
            {
                return Ok(planetService.CreatePlanet(request));
            }
            catch (ArgumentException e){ return BadRequest(e.Message); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlanet(int id)
        {
            try
            {
                return Ok(planetService.DeletePlanet(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public IActionResult UpdatePlanet(int id, UpdatePlanetRequest request)
        {
            try
            {
                return Ok(planetService.UpdatePlanet(id, request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
    }
}
