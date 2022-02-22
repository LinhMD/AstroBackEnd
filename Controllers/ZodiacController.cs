using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AstroBackEnd.Controllers
{
    [Route("api/[controller]")]
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

        [HttpGet]
        
        public IActionResult GetZodiac(int id)
        {
            return Ok(_zodiacService.GetZodiac(id));
        }

        [HttpGet]
        [Route("GetAllZodiac")]
        public IActionResult GetAllZodiac()
        {
            Func<Zodiac,ZodiacView> maping = Zodiac =>
            {
                return new ZodiacView()
                {
                    Id = Zodiac.Id,
                    Name = Zodiac.Name,
                    ZodiacDayStart = Zodiac.ZodiacDayStart,
                    ZodiacMonthStart = Zodiac.ZodiacMonthEnd,
                    ZodiacDayEnd = Zodiac.ZodiacDayEnd,
                    ZodiacMonthEnd = Zodiac.ZodiacMonthEnd,
                    Icon = Zodiac.Icon,
                    Descreiption = Zodiac.Descreiption,
                    MainContent = Zodiac.MainContent,
                    
                };
            };
            return Ok(_zodiacService.GetAllZodiac().Select(maping));
        }

        private void Func(Zodiac zodiac, ZodiacView zodiacView)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{Name}")]
        [Route("FindZodiac")]
        public IActionResult FindZodiac(FindZodiacRequest request)
        {
            return Ok(_zodiacService.FindZodiac(request));
        }

        [HttpPost]
        public IActionResult CreateZodiac(CreateZodiacRequest request)
        {
            return Ok(_zodiacService.CreateZodiac(request));
        }



        [HttpDelete("{id}")]
        public IActionResult ReomoveZodiac(int id)
        {
            return Ok(_zodiacService.RemoveZodiac(id));
        }



        [HttpPut]
        public IActionResult UpdateZodiac(int id, UpdateZodiacRequest updateZodiac)
        {
            _zodiacService.UpdateZodiac(id, updateZodiac);
            return Ok();
        }




    }



    
}
