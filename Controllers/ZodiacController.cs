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
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/zodiacs")]
    [ApiController]
    public class ZodiacController : ControllerBase
    {
        private IUnitOfWork _work;
        private IZodiacService _zodiacService;
        private IAstrologyService _astrology;

        private IFirebaseService _firebase;
        public ZodiacController(IUnitOfWork _work, IZodiacService zodiacService, IAstrologyService astrology, IFirebaseService firebase)
        {
            this._work = _work;
            this._zodiacService = zodiacService;
            _astrology = astrology;
            this._firebase = firebase;
        }

        [HttpGet("{id}")]
        public IActionResult GetZodiac(int id)
        {
            try
            {
                return Ok(_zodiacService.GetZodiac(id));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
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
            catch(ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateZodiac(CreateZodiacRequest request)
        {
            try
            {
                return Ok(_zodiacService.CreateZodiac(request));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult ReomoveZodiac(int id)
        {
            try
            {
                Response.Headers.Add("Allow", "GET, POST, PUT");
                return StatusCode(StatusCodes.Status405MethodNotAllowed);
                /*return Ok(_zodiacService.RemoveZodiac(id));*/
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateZodiac(int id, UpdateZodiacRequest updateZodiac)
        {
            try
            {
                return Ok(_zodiacService.UpdateZodiac(id, updateZodiac));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("natal")]
        [Authorize(Roles = "admin")]
        public IActionResult getBirthChart(DateTime date, double longtitude, double latitude)
        {
            try
            {
                var result = _astrology.GetHousePosOfPlanets(date, longtitude, latitude);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("natal2")]
        public IActionResult GetHouseSnapshot(DateTime date, double longtitude, double latitude)
        {
            try
            {
                return Ok(_astrology.GetPlanetPosition(date, longtitude, latitude));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet("chart")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
        public async Task<IActionResult> GetChartAsync(DateTime date, double longtitude, double latitude)
        {
            try
            {
                /* var file = this._astrology.GetChartStream(date, longtitude, latitude);*/

                var fileName = this._astrology.GetChartFile(date, longtitude, latitude);


                var file = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

                string link = await _firebase.UploadChart(file, fileName.Split('\\').Last());
                file.Close();
                System.IO.File.Delete(fileName);

                return Ok(link);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    } 
}
