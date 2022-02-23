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
    [Route("api/v1.0/[controller]")]
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

        [HttpGet]
        public IActionResult FindHouses(int id, string name, string title, string description, string tag, string mainContent, string sortBy)
        {
            PagingRequest pagingRequest = new PagingRequest()
            {
                SortBy = sortBy,
                Page = 1,
                PageSize = 20,
            };
            FindHouseRequest request = new FindHouseRequest()
            {
                Id = id,
                Name = name,
                Title = title,
                Description = description,
                Tag = tag,
                MainContent = mainContent,
                PagingRequest = pagingRequest,

            };
            
            return Ok(houseService.FindHouse(request));
        }

        [HttpPost]
        public IActionResult CreateHouse(CreateHouseRequest request)
        {
            return Ok(houseService.CreateHouse(request));
        }

        [HttpDelete]
        public IActionResult DeleteHouse(int id)
        {
            return Ok(houseService.DeleteHouse(id));
        }

        [HttpPut]
        public IActionResult UpdateHouse(int id, UpdateHouseRequest request)
        {
            return Ok(houseService.UpdateHouse(id, request));
        }
    }
}
