using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Repositories.Core;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.QuoteRequest;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;

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
                if (request.HoroscopeId > 0)
                {
                    Quote quote = new Quote()
                    {
                        Content = request.Content,
                        HoroscopeId = request.HoroscopeId,
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

        public IEnumerable<Quote> FindQuote(FindQuoteRequest request)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<Quote, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkContent = true;
                    bool checkHoroscopeId = true;
                    if (request.Id > 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Content))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Content))
                        {
                            checkContent = p.Content.Contains(request.Content);
                        }
                        else
                        {
                            checkContent = false;
                        }
                    }
                    if (request.HoroscopeId > 0)
                    {
                        checkHoroscopeId = p.HoroscopeId == request.HoroscopeId;
                    }
                    return checkId && checkContent && checkHoroscopeId;
                };
                PagingRequest paging = request.PagingRequest;
                Validation.ValidNumberThanZero(paging.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(paging.PageSize, "PageSize must be than zero");
                if (paging == null || paging.SortBy == null)
                {
                    return _work.Quotes.Find(filter, p => p.Id);
                }
                else
                {
                    switch (paging.SortBy)
                    {
                        case "HoroscopeId":
                            return _work.Quotes.FindPaging(filter, p => p.HoroscopeId, paging.Page, paging.PageSize);
                        default:
                            return _work.Quotes.FindPaging(filter, p => p.Id, paging.Page, paging.PageSize);
                    }
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
                if (request.HoroscopeId > 0)
                {
                    quote.HoroscopeId = request.HoroscopeId;
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
