using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.ZodiacHouseRequest;
using AstroBackEnd.Services.Core;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/zodiachouse")]
    [ApiController]
    public class ZodiacHouseController : Controller
    {
        
        IUnitOfWork _work;
        IZodiacHouseService iZodiacHouseService;

        public ZodiacHouseController(IUnitOfWork _work, IZodiacHouseService zodiacHouseService)
        {
            this.iZodiacHouseService = zodiacHouseService;
            this._work = _work;
        }

        [HttpGet("{id}")]
        public IActionResult GetZodiacHouse(int id)
        {
            try
            {
                return Ok(iZodiacHouseService.GetZodiacHouse(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            
        }

        [HttpPost]
        public IActionResult CreateZodiacHouse(CreateZodiacHouseRequest request)
        {
            try
            {
                return Ok(iZodiacHouseService.CreateZodiacHouse(request));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public IActionResult FindZodiacHouse(int id, int zodiacId, int houseId, string content, string sortBy, int page = 1, int pagaSize = 20)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pagaSize
                };
                FindZodiacHouse request = new FindZodiacHouse()
                {
                    Id = id,
                    ZodiacId = zodiacId,
                    HouseId = houseId,
                    Content = content,
                    PagingRequest = pagingRequest,

                };
                return Ok(iZodiacHouseService.FindZodiacHouse(request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public IActionResult UpdateZodiacHouse(int id, UpdateZodiacHouseRequest request)
        {
            try
            {
                return Ok(iZodiacHouseService.UpdateZodiacHouse(id, request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }   
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteZodiacHouse(int id)
        {
            try
            {
                return Ok(iZodiacHouseService.DeleteZodiacHouse(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
    }
}
