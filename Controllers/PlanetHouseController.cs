﻿using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetHouseRequest;
using AstroBackEnd.Services.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/planethouse")]
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

        [HttpGet("id")]
        public IActionResult GetPlanetHouse(int id)
        {
            PlanetHouse planetHouse = planetHouseService.GetPlanetHouse(id);
            if (planetHouse == null)
                return NotFound();
            else
                return Ok(planetHouse);
        }

        [HttpGet]
        public IActionResult FindPlanetZodiac(int id, string planetId, string HouseId, string content, string sortBy, int page, int pageSize)
        {
            try
            {
                int checkplanetId = 0;
                int checkHouseId = 0;
                if (!string.IsNullOrWhiteSpace(planetId))
                {
                    checkplanetId = Int32.Parse(planetId);
                }
                if (!string.IsNullOrWhiteSpace(HouseId))
                {
                    checkHouseId = Int32.Parse(HouseId);
                }
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };

                FindPlanetHouseRequest findPlanetZodiacRequest = new FindPlanetHouseRequest()
                {
                    Id = id,
                    Content = content,
                    PlanetId = checkplanetId,
                    HouseId = checkHouseId,
                    PagingRequest = pagingRequest
                };
                return Ok(planetHouseService.FindPlanetHouse(findPlanetZodiacRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreatePlanetZodiac(CreatePlanetHouseRequest request)
        {
            try
            {
                return Ok(planetHouseService.CreatePlanetHouse(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeletePlanetZodiac(int id)
        {
            PlanetHouse planetHouse = planetHouseService.DeletePlanetHouse(id);
            if (planetHouse == null)
                return NotFound();
            else
                return Ok(planetHouse);

        }

        [HttpPut]
        public IActionResult UpdatePlanet(int id, UpdatePlanetHouseRequest request)
        {
            PlanetHouse planetHouse = planetHouseService.UpdatePlanetHouse(id, request);
            if (planetHouse == null)
            {
                return NotFound();
            }
            return Ok(planetHouse);
        }
    }
}