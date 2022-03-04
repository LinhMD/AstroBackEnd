using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/zodiac")]
    [ApiController]
    public class ZodiacController : ControllerBase
    {
        private IUnitOfWork _work;
        private IZodiacService _zodiacService;

        public ZodiacController(IUnitOfWork _work, IZodiacService zodiacService)
        {
            this._work = _work;
            this._zodiacService = zodiacService;
        }

        [HttpGet("{id}")]
        public IActionResult GetZodiac(int id)
        {
            try
            {
                return Ok(_zodiacService.GetZodiac(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
            
        }

        [HttpGet]
        public IActionResult FindZodiac(int id, string name, string sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize
                };

                FindZodiacRequest request = new FindZodiacRequest()
                {
                    Id = id,
                    Name = name,
                    PagingRequest = pagingRequest,
                };
                return Ok(_zodiacService.FindZodiac(request));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateZodiac(CreateZodiacRequest request)
        {
            try
            {
                return Ok(_zodiacService.CreateZodiac(request));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult ReomoveZodiac(int id)
        {
            try
            {
                return Ok(_zodiacService.RemoveZodiac(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public IActionResult UpdateZodiac(int id, UpdateZodiacRequest updateZodiac)
        {
            try
            {
                return Ok(_zodiacService.UpdateZodiac(id, updateZodiac));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
    } 
}
