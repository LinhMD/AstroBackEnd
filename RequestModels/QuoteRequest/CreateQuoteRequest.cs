namespace AstroBackEnd.RequestModels.QuoteRequest
{
    public class CreateQuoteRequest
    {
        public string Content { get; set; }

        public int ZodiacId { get; set; }
    }
}
