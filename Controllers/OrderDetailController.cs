using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.OrderDetailRequest;
using AstroBackEnd.Services.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {

        private readonly IOrderDetailService _detailService;

        public OrderDetailController(IOrderDetailService detailService)
        {
            this._detailService = detailService;
        }


        [HttpGet]
        public IActionResult FindOrderDetail(   int? OrderId, 
                                                string? ProductName, 
                                                double? TotalPriceStart,
                                                double? TotalPriceEnd, 
                                                int? QuantityStart,
                                                int? QuantityEnd, 
                                                string? ReviewMessage, 
                                                DateTime? ReviewDateStart, 
                                                DateTime? ReviewDateEnd,
                                                string? sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                IEnumerable<Models.OrderDetail> details = _detailService.FindOrderDetail(new FindOrderDetailRequest() 
                { 
                    OrderId = OrderId,
                    ProductName = ProductName,
                    TotalPriceStart = TotalPriceStart,
                    TotalPriceEnd = TotalPriceEnd,
                    QuantityStart = QuantityStart,
                    QuantityEnd = QuantityEnd,
                    ReviewMessage = ReviewMessage,
                    ReviewDateStart= ReviewDateStart,
                    ReviewDateEnd = ReviewDateEnd,
                    Paging = new PagingRequest()
                    {
                        Page = page,
                        PageSize = pageSize,
                        SortBy = sortBy
                    }
                    
                
                });
                return Ok(details);
            }
            catch 
            {
                return BadRequest();
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetail(int id)
        {
            try
            {
                var detail = _detailService.GetOrderDetail(id);
                return Ok(detail);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateOrderDetail(OrderDetailCreateRequest request)
        {
            var detail = _detailService.CreateOrderDetail(request);
            return Ok(detail);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrderDetail(int id, OrderDetailUpdateRequest request)
        {
            try
            {
                var detail = _detailService.UpdateOrderDetail(id, request);
                return Ok(detail);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteOrderDetail(int id)
        {
            try
            {
                _detailService.DeleteOrderDetail(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
