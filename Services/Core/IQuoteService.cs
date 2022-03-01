using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.QuoteRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Repositories.Core
{
    public interface IQuoteService
    {
        public Quote CreateQuote(CreateQuoteRequest request);
        public IEnumerable<Quote> FindQuote(FindQuoteRequest request);
        public Quote GetQuote(int id);
        public Quote UpdateQuote(int id, UpdateQuoteRequest request);
        public Quote DeleteQuote(int id);
    }
}
