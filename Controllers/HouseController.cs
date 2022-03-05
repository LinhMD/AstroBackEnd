﻿using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Repositories.Core;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.HouseRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/house")]
    [ApiController]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService houseService;
        private readonly IUnitOfWork _work;

        public HouseController(IHouseService houseService, IUnitOfWork _work)
        {
            this.houseService = houseService;
            this._work = _work;
        }

        [HttpGet("{id}")]
        public IActionResult GetHouse(int id)
        {
            try
            {
                return Ok(houseService.GetHouse(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        public IActionResult FindHouses(int id, string name, string title, string description, string tag, string mainContent, string sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindHouseRequest request = new FindHouseRequest()
                {
                    Id = id,
                    Name = name,
                    Title = title,
                    Description = description,
                    Tag = tag,
                    MainContent = mainContent,
                    PagingRequest = pagingRequest,

                };
                return Ok(houseService.FindHouse(request));
            }catch (ArgumentException ex) { return BadRequest(ex.Message); }
            
        }

        [HttpPost]
        public IActionResult CreateHouse(CreateHouseRequest request)
        {
            try
            {
                return Ok(houseService.CreateHouse(request));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHouse(int id)
        {
            try
            {
                return Ok(houseService.DeleteHouse(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public IActionResult UpdateHouse(int id, UpdateHouseRequest request)
        {
            try
            {
                return Ok(houseService.UpdateHouse(id, request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
    }
}
