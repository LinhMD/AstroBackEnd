using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.HoroscopeItemRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/horoscopeitems")]
    [ApiController]
    public class HoroscopeItemController : ControllerBase
    {
        private readonly IUnitOfWork _work;
        private IHoroscopeItemService horoscopeItemService;
        public HoroscopeItemController(IUnitOfWork _work, IHoroscopeItemService horoscopeItemService)
        {
            this._work = _work;
            this.horoscopeItemService = horoscopeItemService;
        }

        [HttpPost]
        public IActionResult CreateHoroscope(CreateHoroscopeItemRequest request)
        {
            try 
            { 
                return Ok(horoscopeItemService.CreateHoroscopeItem(request));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetHoroscopeItem(int id)
        {
            try
            {
                return Ok(new HoroscopeItemView(horoscopeItemService.GetHoroscopeItem(id)));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public IActionResult FindHoroscopeItem(int id, int aspectId, int lifeAttributeId, int value, string sortBy, int page = 1, int pageSize = 20)
        {
            try { 
                int total = 0;
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindHoroscopeItemRequest findHoroscopeItemRequest = new FindHoroscopeItemRequest()
                {
                    Id = id,
                    AspectId = aspectId,
                    LifeAttributeId = lifeAttributeId,
                    Value = value,
                    PagingRequest = pagingRequest
                };
                var findResult = horoscopeItemService.FindHoroscopeItem(findHoroscopeItemRequest, out total).Select(hororscopeItem => new HoroscopeItemView(hororscopeItem));
                PagingView pagingView = new PagingView()
                {
                    Payload = findResult,
                    Total = total
                };
                return Ok(pagingView);
            }catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
        [HttpPut]
        public IActionResult UpdateHoroscopeItem(int id, UpdateHoroscopeItemRequest updateHoroscopeItem) 
        {
            try
            {
                return Ok(horoscopeItemService.UpdateHoroscopeItem(id, updateHoroscopeItem));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHoroscopeItem(int id)
        {
            try
            {
                return Ok(horoscopeItemService.DeleteHoroscopeItem(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
    }
}
