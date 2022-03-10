using AstroBackEnd.Models;

namespace AstroBackEnd.ViewsModel
{
    public class PlanetHouseView
    {
        public PlanetHouseView(PlanetHouse planetHouse)
        {
            Id = planetHouse.Id;
            HouseId = planetHouse.HouseId;
            HouseName = planetHouse.House.Name;
            PlanetId = planetHouse.PlanetId;
            PlanetName = planetHouse.Planet.Name;
            Content = planetHouse.Content;
        }

        public int Id { get; set; }

        public string HouseName { get; set; }

        public int HouseId { get; set; }

        public string PlanetName { get; set; }

        public int PlanetId { get; set; }

        public string Content { get; set; }
    }
}
