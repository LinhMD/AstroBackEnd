namespace AstroBackEnd.RequestModels.LifeAttributeRequest
{
    public class FindLifeAttributeRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public PagingRequest PagingRequest { get; set; }
    }
}
