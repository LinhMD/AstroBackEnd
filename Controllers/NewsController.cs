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
using AstroBackEnd.RequestModels;
using AstroBackEnd.Repositories;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/news")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private INewsService _Service;
        private IUnitOfWork _work;
        public NewsController(INewsService service, IUnitOfWork work)
        {
            this._Service = service;
            this._work = work;
        }

        [HttpGet("{id}")]
        public IActionResult GetNew(int id)
        {
            var result = _Service.GetNews(id);
            if (result==null)
            {
                return BadRequest(new { StatusCodes = 404, Message = "New not found" });
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

        [HttpPut]
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

        [HttpGet]
        public IActionResult FindNews(string title, string description, string tag,
            string sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindNewsRequest findNewsRequest = new FindNewsRequest()
                {
                    Title = title,
                    Description = description,
                    Tag =tag
                };
                return Ok(_Service.FindNews(findNewsRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNews(int id)
        {
            News news = _work.News.Get(id);
            if (news != null)
            {
                _Service.DeleteNew(id);
                return Ok(news);
            }
            else
            {
                return NotFound();
            }
        }

    }
}
