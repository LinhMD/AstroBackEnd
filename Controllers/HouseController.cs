using AstroBackEnd.Models;
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
    [Route("api/v1/houses")]
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
        public IActionResult FindHouses(int id, string name, string title, string icon, string description, string tag, string sortBy, int page = 1, int pageSize = 20)
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
                FindHouseRequest request = new FindHouseRequest()
                {
                    Id = id,
                    Name = name,
                    Title = title,
                    Icon = icon,

                   
                    Decription = description,

                    Tag = tag,
                    PagingRequest = pagingRequest,

                };
                var finResult = houseService.FindHouse(request, out total);
                PagingView pagingView = new PagingView()
                {
                    Payload = finResult,
                    Total = total
                };
                return Ok(pagingView);
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
