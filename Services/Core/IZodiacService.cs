using AstroBackEnd.Models;
using AstroBackEnd.RequestModels;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface IZodiacService
    {
        public Zodiac GetZodiac(int id);

        public Zodiac CreateZodiac(CreateZodiacRequest request);

        public Zodiac RemoveZodiac(int id);

        public Zodiac UpdateZodiac(int id, UpdateZodiacRequest updateZodiac);

        public IEnumerable<Zodiac> FindZodiac(FindZodiacRequest request);
    }
}
