using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.ZodiacHouseRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface IZodiacHouseService
    {
        public ZodiacHouse GetZodiacHouse(int id);
        public ZodiacHouse CreateZodiacHouse(CreateZodiacHouseRequest request);

        public IEnumerable<ZodiacHouse> FindZodiacHouse(FindZodiacHouse request, out int total);

        public ZodiacHouse UpdateZodiacHouse(int id, UpdateZodiacHouseRequest request);

        public ZodiacHouse DeleteZodiacHouse(int id);

    }
}
