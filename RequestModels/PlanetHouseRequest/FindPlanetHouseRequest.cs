namespace AstroBackEnd.RequestModels.PlanetHouseRequest
{
    public class FindPlanetHouseRequest
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public int PlanetId { get; set; }
        public string Content { get; set; }
        public PagingRequest PagingRequest { get; set; }
    }
}
