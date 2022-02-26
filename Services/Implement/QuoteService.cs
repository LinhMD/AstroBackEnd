using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Repositories.Core;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.QuoteRequest;
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
            if(request.HoroscopeId < 1)
            {
                 throw new Exception("HoroscopeId cannot empty and must be than zero");

            }
            else
            {
                Quote quote = new Quote()
                {
                    Content = request.Content,
                    HoroscopeId = request.HoroscopeId,
                };
                return _work.Quotes.Add(quote); ;
            }
        }

        public IEnumerable<Quote> FindQuote(FindQuoteRequest request)
        {
            Func<Quote, bool> filter = p =>
            {
                bool checkId = true;
                bool checkContent = true;
                bool checkHoroscopeId = true;
                if (request.Id != 0)
                {
                    if (p.Id == request.Id)
                    {
                        checkId = true;
                    }
                    else
                    {
                        checkId = false;
                    }
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
                    if (p.HoroscopeId > 0)
                    {
                        checkHoroscopeId = p.HoroscopeId == request.HoroscopeId;
                    }
                    else
                    {
                        checkHoroscopeId = false;
                    }
                }
                return checkId && checkContent && checkHoroscopeId;
            };

            PagingRequest paging = request.PagingRequest;
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
        }

        public Quote DeleteQuote(int id)
        {
            Quote quote = _work.Quotes.Get(id);
            if (quote == null)
                return null;
            else
            {
                _work.Quotes.Remove(quote);
                return quote;
            }

        }

        public Quote GetQuote(int id)
        {
            return _work.Quotes.Get(id);
        }

        public Quote UpdateQuote(int id, UpdateQuoteRequest request)
        {
            Quote quote = _work.Quotes.Get(id);
            if (quote != null)
            {
                if (request.HoroscopeId > 0)
                {
                    quote.HoroscopeId = request.HoroscopeId;
                }

                return quote;
            }
            return null;
        }
        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
