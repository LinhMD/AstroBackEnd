using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.PlanetRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface IPlanetService
    {
        public Planet GetPlanet(int id);
        public Planet CreatePlanet(CreatePlanetRequest request);
        public IEnumerable<Planet> FindPlanet(FindPlanetRequest request);
        public Planet DeletePlanet(int id);
        public Planet UpdatePlanet(int id, UpdatePlanetRequest request);
    }
}
