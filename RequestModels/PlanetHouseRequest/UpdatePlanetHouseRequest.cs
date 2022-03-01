namespace AstroBackEnd.RequestModels.PlanetHouseRequest
{
    public class UpdatePlanetHouseRequest
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public int PlanetId { get; set; }
        public string Content { get; set; }
    }
}
