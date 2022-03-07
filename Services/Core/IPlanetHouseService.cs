using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.PlanetHouseRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface IPlanetHouseService
    { 
        public PlanetHouse GetPlanetHouse(int id);
        public PlanetHouse CreatePlanetHouse(CreatePlanetHouseRequest request);
        public IEnumerable<PlanetHouse> FindPlanetHouse(FindPlanetHouseRequest request);
        public PlanetHouse DeletePlanetHouse(int id);
        public PlanetHouse UpdatePlanetHouse(int id, UpdatePlanetHouseRequest request);
    }
}
