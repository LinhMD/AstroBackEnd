
namespace AstroBackEnd.RequestModels.PlanetHouseRequest
{
    public class CreatePlanetHouseRequest
    {
        public int Id { get; set; }
        public int HouseId { get; set; }
        public int PlanetId { get; set; }
        public string Content { get; set; }
    }
}
