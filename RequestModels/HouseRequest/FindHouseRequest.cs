namespace AstroBackEnd.RequestModels.HouseRequest
{
    public class FindHouseRequest
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Title { get; set; }
        public string Decription { get; set; }
        public string? Tag { get; set; }
        public PagingRequest? PagingRequest { get; set; }
    }
}
