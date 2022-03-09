namespace AstroBackEnd.RequestModels.QuoteRequest
{
    public class FindQuoteRequest
    {
        public int Id { get; set; }

        public int HoroscopeId { get; set; }

        public PagingRequest PagingRequest { get; set; }
    }
}
