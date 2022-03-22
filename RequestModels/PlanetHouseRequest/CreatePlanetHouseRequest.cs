
namespace AstroBackEnd.RequestModels.PlanetHouseRequest
{
    public class CreatePlanetHouseRequest
    {
        public int HouseId { get; set; }
        public int PlanetId { get; set; }
        public string Content { get; set; }
    }
}
