using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.HoroscopeRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/horoscopes")]
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
            try
            {
                return Ok(horoscopeService.GetHoroscope(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        public IActionResult FindHoroscope(int id, string colorLuck, float numberLuck, string love, string money, string sortBy, int page = 1, int pageSize = 20)
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
                FindHoroscopeRequest findHoroscopeRequest = new FindHoroscopeRequest()
                {
                    Id = id,
                    ColorLuck = colorLuck,
                    Love = love,
                    Money = money,
                    NumberLuck = numberLuck,
                    PagingRequest = pagingRequest
                };
                var findResult = horoscopeService.FindHoroscope(findHoroscopeRequest, out total);
                PagingView pagingView = new PagingView()
                {
                    Payload = findResult,
                    Total = total
                };
                return Ok(pagingView);
            }catch (ArgumentException ex)
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
            }
            catch(ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateHoroscope(int id, UpdateHoroscopeRequest request)
        {
            try
            {
                Horoscope horoscope = horoscopeService.UpdateHoroscope(id, request);
                if (horoscope != null)
                {
                    return Ok(horoscope);
                }
                Validation.Validate(horoscope);
                return Ok(horoscope);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteHoroscope(int id)
        {
            try
            {
                return Ok(horoscopeService.DeleteHoroscope(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

    }
}
