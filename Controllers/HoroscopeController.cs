using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.HoroscopeRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/horoscope")]
    [ApiController]
    public class HoroscopeController : ControllerBase
    {
        private readonly IUnitOfWork _work;
        private IHoroscopeService horoscopeService;
        
        public HoroscopeController(IUnitOfWork _work, IHoroscopeService horoscopeService)
        {
            this._work = _work;
            this.horoscopeService = horoscopeService;
        }

        [HttpGet("{id}")]
        public IActionResult GetHoroscope(int id)
        {
            Horoscope horoscope = horoscopeService.GetHoroscope(id);
            if(horoscope != null)
                return Ok(horoscope);
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult FindHoroscope(int id, string colorLuck, float numberLuck, string love, string money, string sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
               
                FindHoroscopeRequest findHoroscopeRequest = new FindHoroscopeRequest()
                {
                    Id = id,
                    ColorLuck = colorLuck,
                    Love = love,
                    Money = money,
                    NumberLuck = numberLuck,
                    PagingRequest = pagingRequest
                };
                return Ok(horoscopeService.FindHoroscope(findHoroscopeRequest));
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateHoroscope(CreateHoroscopeRequest request)
        {
            try
            {
                return Ok(horoscopeService.CreateHoroscope(request));
            }catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateHoroscope(int id, UpdateHoroscopeRequest request)
        {
            Horoscope horoscope = horoscopeService.UpdateHoroscope(id, request);
            if(horoscope != null)
            {
                 return Ok(horoscope);
            }
            Validation.Validate(horoscope);
            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHoroscope(int id)
        {
            Horoscope horoscope = horoscopeService.DeleteHoroscope(id);
            if (horoscope != null)
                return Ok(horoscope);
            else
                return NotFound();
        }

    }
}
