using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Repositories.Core;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.QuoteRequest;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/quotes")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private IUnitOfWork _work;
        private IQuoteService quoteService;
        public QuoteController(IUnitOfWork _work, IQuoteService quoteService)
        {
            this._work = _work;
            this.quoteService = quoteService;
        }

        [HttpGet("{id}")]
        public IActionResult GetQuote(int id)
        {
            try
            {
                return Ok(quoteService.GetQuote(id));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult FindQuote(int id, int zodiacId, string sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                int total = 0;
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindQuoteRequest findQuoteRequest = new FindQuoteRequest()
                {
                    Id = id,
                    ZodiacId = zodiacId,
                    PagingRequest = pagingRequest
                };
                var findResult = quoteService.FindQuote(findQuoteRequest, out total);
                PagingView pagingView = new PagingView()
                {
                    Payload = findResult,
                    Total = total
                };
                return Ok(pagingView);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateQuote(CreateQuoteRequest request)
        {
            try
            {
                return Ok(quoteService.CreateQuote(request));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = "admin")]
        [HttpPut]
        public IActionResult UpdateQuote(int id, UpdateQuoteRequest request)
        {
            try
            {
                return Ok(quoteService.UpdateQuote(id, request));
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
        public IActionResult DeleteQuote(int id)
        {
            try
            {
                return Ok(quoteService.DeleteQuote(id));
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
