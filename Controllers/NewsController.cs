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
            try
            {
                return Ok(_Service.GetNews(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }

        }

        [HttpPost]
        public IActionResult CreateNew([FromBody] NewsCreateRequest request)
        {
            try
            {
                return Ok(_Service.CreateNew(request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpPut]
        public IActionResult UpdateNew(int id, [FromBody] NewsUpdateRequest request)
        {

            try
            {
                return Ok(_Service.UpdateNew(id,request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNews(int id)
        {
            try
            {
                return Ok(_Service.DeleteNew(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

    }
}
