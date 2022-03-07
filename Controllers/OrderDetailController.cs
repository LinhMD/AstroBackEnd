using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.OrderDetailRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/detail")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {

        private readonly IOrderDetailService _detailService;

        public OrderDetailController(IOrderDetailService detailService)
        {
            this._detailService = detailService;
        }


        [HttpGet]
        public IActionResult FindOrderDetail(   int? orderId, 
                                                string? productName, 
                                                double? totalPriceStart,
                                                double? totalPriceEnd, 
                                                int? quantityStart,
                                                int? quantityEnd, 
                                                string? reviewMessage, 
                                                DateTime? reviewDateStart, 
                                                DateTime? reviewDateEnd,
                                                string? sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                int total = 0;
                IEnumerable<Models.OrderDetail> details = _detailService.FindOrderDetail(new FindOrderDetailRequest() 
                { 
                    OrderId = orderId,
                    ProductName = productName,
                    TotalPriceStart = totalPriceStart,
                    TotalPriceEnd = totalPriceEnd,
                    QuantityStart = quantityStart,
                    QuantityEnd = quantityEnd,
                    ReviewMessage = reviewMessage,
                    ReviewDateStart= reviewDateStart,
                    ReviewDateEnd = reviewDateEnd,
                    Paging = new PagingRequest()
                    {
                        Page = page,
                        PageSize = pageSize,
                        SortBy = sortBy
                    }
                }, out total);
                return Ok(new PagingView() { Payload = details, Total = total });
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

        [HttpPut]
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

        [HttpDelete("{id}")]
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
