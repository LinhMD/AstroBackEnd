using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Repositories.Core;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.QuoteRequest;
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



        [HttpGet("id")]
        public IActionResult GetQuote(int id)
        {
            Quote quote = quoteService.GetQuote(id);
            if (quote != null)
                return Ok(quote);
            else
                return NotFound();
        }

        [HttpGet]
        public IActionResult FindQuote(int id, string content, int horoscopeId, string sortBy, int page, int pageSize)
        {
            try
            {
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
                return Ok(quoteService.FindQuote(findQuoteRequest));
            }
            catch (Exception ex)
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
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateQuote(int id, UpdateQuoteRequest request)
        {
            Quote quote = quoteService.UpdateQuote(id, request);
            if (quote != null)
            {
                return Ok(quote);
            }
            return NotFound();
        }

        [HttpDelete]
        public IActionResult DeleteQuote(int id)
        {
            Quote quote = quoteService.DeleteQuote(id);
            if (quote != null)
                return Ok(quote);
            else
                return NotFound();
        }
    }
}
