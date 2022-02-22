namespace AstroBackEnd.RequestModels
{
    public class FindZodiacRequest
    {

        public string? Name { get; set; }

        public PagingRequest? PagingRequest { get; set; }
    }
}
