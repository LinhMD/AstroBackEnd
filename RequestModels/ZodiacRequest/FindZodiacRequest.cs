namespace AstroBackEnd.RequestModels
{
    public class FindZodiacRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PagingRequest PagingRequest { get; set; }
    }
}
