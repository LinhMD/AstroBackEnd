using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/zodiacs")]
    [ApiController]
    public class ZodiacController : ControllerBase
    {
        private IUnitOfWork _work;
        private IZodiacService _zodiacService;
        private AstrologyUtil Astrology;
        public ZodiacController(IUnitOfWork _work, IZodiacService zodiacService, AstrologyUtil astrology)
        {
            this._work = _work;
            this._zodiacService = zodiacService;
            Astrology = astrology;
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
                int total = 0;
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

                var result = _zodiacService.FindZodiac(request, out total).Select(zodiac => new ZodiacView(zodiac));
                PagingView pagingView = new PagingView()
                {
                    Payload = result,
                    Total = total,
                };
                return Ok(pagingView);
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
            }catch (ArgumentException ex)
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
        [HttpGet("natal")]
        public IActionResult getBirthChart(DateTime date, double longtitude, double latitude)
        {
            try
            {
                var result = Astrology.GetHousePosOfPlanets(date, longtitude, latitude);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("natal2")]
        public IActionResult GetHouseSnapshot(DateTime date, double longtitude, double latitude)
        {
            try
            {
                return Ok(Astrology.GetHouseSnapshot(date, longtitude, latitude));
            }
            catch (Exception e)
            {
                return BadRequest(e.StackTrace);
            }
        }

        [HttpGet("chart")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public IActionResult GetChart(DateTime date, double longtitude, double latitude)
        {
            try
            {
                System.Drawing.Image chart = Astrology.GetChart(date, longtitude, latitude);
                MemoryStream ms = new MemoryStream();
                chart.Save(ms, ImageFormat.Png);
                var file = new FileStream(@$"resource\chart-{date.DayOfYear}-{(int)longtitude}-{(int)latitude}.png", FileMode.OpenOrCreate, FileAccess.Write);

                chart.Save(file, ImageFormat.Png);
                file.Close();
                return Ok();
            } 
            catch (Exception e)
            {
                return BadRequest(e.StackTrace);
            }
        }
    } 
}
