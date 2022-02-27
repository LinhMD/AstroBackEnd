using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.PlanetZodiacRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface IPlanetZodiacService
    {
        public PlanetZodiac GetPlanetZodiac(int id);
        public PlanetZodiac CreatePlanetZodiac(CreatePlanetZodiacRequest request);
        public IEnumerable<PlanetZodiac> FindPlanetZodiac(FindPlanetZodiacRequest request);
        public PlanetZodiac DeletePlanetZodiac(int id);
        public PlanetZodiac UpdatePlanetZodiac(int id, UpdatePlanetZodiacRequest request);
    }
}
