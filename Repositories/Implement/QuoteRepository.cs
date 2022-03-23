using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using System.Linq;

namespace AstroBackEnd.Repositories.Implement
{
    public class QuoteRepository : Repository<Quote>, IQuoteRepository
    {
        public QuoteRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }

        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public override IQueryable<Quote> WithAllData()
        {
            return AstroData.Quotes.AsQueryable();
        }
    }
}
