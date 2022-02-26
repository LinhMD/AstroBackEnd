using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.HoroscopeRequest;
using AstroBackEnd.Services.Core;
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

        [HttpGet("id")]
        public IActionResult GetHoroscope(int id)
        {
            Horoscope horoscope = horoscopeService.GetHoroscope(id);
            if(horoscope == null)
                return NotFound();
            else 
                return Ok(horoscope);
        }

        [HttpGet]
        public IActionResult FindHoroscope(int id, string colorLuck, string numberLuck, string work, string love, string money, string sortBy, int page, int pageSize)
        {
            try
            {
                float checkNumberLuck = 0;
                if (!string.IsNullOrWhiteSpace(numberLuck))
                {
                    checkNumberLuck = float.Parse(numberLuck);
                }
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
                    NumberLuck = checkNumberLuck,
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
            if(horoscope == null)
            {
                return NotFound();
            }
            return Ok(horoscope);
        }

        [HttpDelete]
        public IActionResult DeleteHoroscope(int id)
        {
            Horoscope horoscope = horoscopeService.DeleteHoroscope(id);
            if (horoscope == null)
                return NotFound();
            else
                return Ok(horoscope);
        }

    }
}
