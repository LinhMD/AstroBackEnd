using AstroBackEnd.RequestModels.OrderRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            try
            {
                var order = _orderService.CreateOrder(request);
                return Ok(order);
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
        public IActionResult UpdateOrder(int id, CreateOrderRequest request)
        {
            try
            {
                var order = _orderService.UpdateOrder(id, request);
                return Ok(order);
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
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult GetOrder(int id)
        {

            try
            {
                var order = _orderService.GetOrder(id);
                return order == null? BadRequest("Order id not found") : Ok(order);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        public IActionResult FindOrder( int? status, 
                                        DateTime? orderTimeStart, DateTime? orderTimeEnd,
                                        double? totalCostStart, double? totalCostEnd, 
                                        string? deliveryAdress, 
                                        string? deleveryPhone, 
                                        int? userId,
                                        string? sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                int total = 0;
                var orders = _orderService.FindOrder(new FindOrderRequest() { 
                    Status = status,
                    OrderTimeEnd = orderTimeEnd,
                    OrderTimeStart = orderTimeStart,
                    TotalCostEnd = totalCostEnd,
                    TotalCostStart = totalCostStart,
                    DeliveryAdress = deliveryAdress,
                    DeleveryPhone = deleveryPhone,
                    UserId = userId,

                    PagingRequest = new RequestModels.PagingRequest()
                    {
                        Page = page,
                        PageSize = pageSize,
                        SortBy = sortBy
                    }               
                }, out total);
                return Ok(new PagingView() { Payload = orders, Total = total});
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
