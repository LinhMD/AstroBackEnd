using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.ZodiacHouseRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface IZodiacHouseService
    {
        public ZodiacHouse CreateZodiacHouse(CreateZodiacHouseRequest request);

        public IEnumerable<ZodiacHouse> FindZodiacHouse(FindZodiacHouse request);

        public ZodiacHouse UpdateZodiacHouse(int id, UpdateZodiacHouseRequest request);

        public ZodiacHouse DeleteZodiacHouse(int id);

    }
}
