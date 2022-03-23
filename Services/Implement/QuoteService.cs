using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Repositories.Core;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.QuoteRequest;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Services.Implement
{
    public class QuoteService : IQuoteService, IDisposable
    {
        private readonly IUnitOfWork _work;

        public QuoteService(IUnitOfWork _work)
        {
            this._work = _work;
        }
        public Quote CreateQuote(CreateQuoteRequest request)
        {
            try
            {
                if (request.ZodiacId > 0)
                {
                    Quote quote = new Quote()
                    {
                        Content = request.Content,
                        ZodiacId = request.ZodiacId,
                    };
                    return _work.Quotes.Add(quote);
                }
                else
                {
                    throw new ArgumentException("HoroscopeId cannot empty and must be than zero");
                }
            }
            catch(Exception ex)
            {
                throw new ArgumentException("QuoteService : " + ex.Message);
            }
        }

        public IEnumerable<Quote> FindQuote(FindQuoteRequest request, out int total)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<Quote, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkHoroscopeId = true;
                    if (request.Id > 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (request.ZodiacId > 0)
                    {
                        checkHoroscopeId = p.ZodiacId == request.ZodiacId;
                    }
                    return checkId && checkHoroscopeId;
                };
                PagingRequest pagingRequest = request.PagingRequest;
                Validation.ValidNumberThanZero(pagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(pagingRequest.PageSize, "PageSize must be than zero");
                if (pagingRequest != null)
                {
                    switch (pagingRequest.SortBy)
                    {
                        case "HoroscopeId":
                            return _work.Quotes.FindPaging(filter, p => p.ZodiacId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        default:
                            return _work.Quotes.FindPaging(filter, p => p.Id, out total, pagingRequest.Page, pagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<Quote> result = _work.Quotes.Find(filter, p => p.Id);
                    total = result.Count();
                    return result;
                }
            } catch (Exception ex) { throw new ArgumentException("QuoteService : " + ex.Message); }
        }

        public Quote DeleteQuote(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Quote quote = _work.Quotes.Get(id);
            if (quote != null)
            {
                _work.Quotes.Remove(quote);
                return quote;
            }
            else { throw new ArgumentException("This Quote not found"); }
        }

        public Quote GetQuote(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Quote quote = _work.Quotes.Get(id);
            if (quote != null)
            {
                return _work.Quotes.Get(id);
            }
            else { throw new ArgumentException("This Quote not found"); }
        }

        public Quote UpdateQuote(int id, UpdateQuoteRequest request)
        {

            Validation.ValidNumberThanZero(id, "Id must be than zero");       
            Quote quote = _work.Quotes.Get(id);
            if (quote != null)
            {
                if (request.ZodiacId > 0)
                {
                    quote.ZodiacId = request.ZodiacId;
                }

                return quote;
            }
            else
            {
                throw new ArgumentException("This Quote not found");
            }
        }
        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
