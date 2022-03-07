using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.HouseRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface IHouseService
    {
        public House GetHouse(int id);
        public House CreateHouse(CreateHouseRequest request);

        public IEnumerable<House> FindHouse(FindHouseRequest request, out int total);

        public House UpdateHouse(int id, UpdateHouseRequest request);

        public House DeleteHouse (int id);
 
    }
}
