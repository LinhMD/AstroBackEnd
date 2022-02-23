using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.ZodiacHouseRequest;
using AstroBackEnd.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1.0/[controller]")]
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

        [HttpPost]
        public IActionResult CreateZodiacHouse(CreateZodiacHouseRequest request)
        {
            return Ok(iZodiacHouseService.CreateZodiacHouse(request));
        }

        [HttpGet]
        public IActionResult FindZodiacHouse(int id, int zodiacId, int houseId, string content, string sortBy)
        {
            PagingRequest pagingRequest = new PagingRequest()
            {
                SortBy = sortBy,
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

        [HttpPut]
        public IActionResult UpdateZodiacHouse(int id, UpdateZodiacHouseRequest request)
        {
            return Ok(iZodiacHouseService.UpdateZodiacHouse(id, request));
        }

        [HttpDelete]
        public IActionResult DeleteZodiacHouse(int id)
        {
            return Ok(iZodiacHouseService.DeleteZodiacHouse(id));
        }
    }
}
