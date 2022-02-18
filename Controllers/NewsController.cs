using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Services.Core;
using AstroBackEnd.RequestModels.NewRequest;

namespace AstroBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _Service;

        public NewsController(INewsService newService)
        {
            this._Service = newService;
        }
        [HttpGet]
        public IActionResult GetAllNew()
        {
            return Ok(_Service.GetAllNew());
        }

        [HttpGet("{id}")]
        public IActionResult GetNew(int id)
        {
            //return Ok(_Service.GetNews(id));
            var result = _Service.GetNews(id);
            if (result==null)
            {
                return BadRequest(new { StatusCodes = 404, Message = "New not found" }); // ok
            }
            else
            {
                return Ok(new { StatusCode = 200, message = "The request has been completed successfully", data = result }); 
            }

        }

        [HttpPost]
        public IActionResult CreateNew([FromBody] NewsCreateRequest request)
        {
            var result = _Service.CreateNew(request);
            if (result == null)
            {
                return BadRequest(new { StatusCodes = 404, Message = "Can't create New" }); 
            }
            else
            {
                return Ok(new { StatusCode = 200, message = "The request has been completed successfully", data = result });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNew(int id, [FromBody] NewsUpdateRequest request)
        {
            
            var checkFindNew = _Service.GetNews(id);
            if (checkFindNew==null)
            {
                return BadRequest(new { StatusCodes = 404, Message = "Can't find New" });
            }
            else
            {
                _Service.UpdateNew(id, request);
                return Ok(new { StatusCode = 200, message = "The request has been completed successfully" });
            }
        }

        [HttpPost]
        [Route("findNew")]
        public IActionResult FindNew([FromBody] FindNewsRequest request)
        {
            var result = _Service.FindNews(request);
            if (result == null || !result.Any() )
            {
                return BadRequest(new { StatusCodes = 404, Message = "Can't find" });
            }
            else
            {
                return Ok(new { StatusCode = 200, message = "The request has been completed successfully", data = result });
            }
        }

    }
}
