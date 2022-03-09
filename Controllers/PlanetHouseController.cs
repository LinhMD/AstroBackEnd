using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetHouseRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/planethouses")]
    [ApiController]
    public class PlanetHouseController : ControllerBase
    {
        private readonly IPlanetHouseService planetHouseService;
        private readonly IUnitOfWork _work;

        public PlanetHouseController(IPlanetHouseService planetHouseService, IUnitOfWork _work)
        {
            this.planetHouseService = planetHouseService;
            this._work = _work;
        }

        [HttpGet("{id}")]
        public IActionResult GetPlanetHouse(int id)
        {
            try
            {
                return Ok(new PlanetHouseView(planetHouseService.GetPlanetHouse(id)));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        public IActionResult FindPlanetZodiac(int id, int planetId, int HouseId, string sortBy, int page = 1, int pageSize = 20)
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

                FindPlanetHouseRequest findPlanetZodiacRequest = new FindPlanetHouseRequest()
                {
                    Id = id,
                    PlanetId = planetId,
                    HouseId = HouseId,
                    PagingRequest = pagingRequest
                };
                var findResult = planetHouseService.FindPlanetHouse(findPlanetZodiacRequest, out total).Select(ph => new PlanetHouseView(ph));
                PagingView pagingView = new PagingView()
                {
                    Payload = findResult,
                    Total = total
                };
                return Ok(pagingView);
            }catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPost]
        public IActionResult CreatePlanetZodiac(CreatePlanetHouseRequest request)
        {
            try
            {
                return Ok(planetHouseService.CreatePlanetHouse(request));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlanetZodiac(int id)
        {
            try
            {
                return Ok(planetHouseService.DeletePlanetHouse(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public IActionResult UpdatePlanet(int id, UpdatePlanetHouseRequest request)
        {
            try
            {
                return Ok(planetHouseService.UpdatePlanetHouse(id, request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
    }
}
