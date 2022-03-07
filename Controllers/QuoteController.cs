using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Repositories.Core;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.QuoteRequest;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/quote")]
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
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpGet]
        public IActionResult FindQuote(int id, string content, int horoscopeId, string sortBy, int page = 1, int pageSize = 20)
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
                    Content = content,
                    HoroscopeId = horoscopeId,
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
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateQuote(CreateQuoteRequest request)
        {
            try
            {
                return Ok(quoteService.CreateQuote(request));
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateQuote(int id, UpdateQuoteRequest request)
        {
            try
            {
                return Ok(quoteService.UpdateQuote(id, request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteQuote(int id)
        {
            try
            {
                return Ok(quoteService.DeleteQuote(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
    }
}
