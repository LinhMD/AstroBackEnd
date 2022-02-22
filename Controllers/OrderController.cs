using AstroBackEnd.RequestModels.OrderRequest;
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
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            this._orderService = orderService;
        }
        [HttpGet]
        public IActionResult GetAllOrder()
        {

            return Ok(_orderService.GetAllOrders());
        }

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderRequest request)
        {
            try
            {
                var order = _orderService.CreateOrder(request);
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, CreateOrderRequest request)
        {
            try
            {
                var order = _orderService.UpdateOrder(id, request);
                return Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            try
            {
                _orderService.DeleteOrder(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOrder(int id)
        {

            try
            {
                var order = _orderService.GetOrder(id);
                return order == null? BadRequest("Order id not found") : Ok(order);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("f")]
        public IActionResult FindOrder(FindOrderRequest request)
        {
            try
            {
                var orders = _orderService.FindOrder(request);
                return Ok(orders);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
