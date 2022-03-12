using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.HoroscopeItemRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface IHoroscopeItemService
    {
        public HoroscopeItem CreateHoroscopeItem(CreateHoroscopeItemRequest createHoroscope);
        public HoroscopeItem GetHoroscopeItem(int id);
        public IEnumerable<HoroscopeItem> FindHoroscopeItem(FindHoroscopeItemRequest findHoroscopeItem, out int total);
        public HoroscopeItem UpdateHoroscopeItem(int id, UpdateHoroscopeItemRequest updateHoroscopeItem);
        public HoroscopeItem DeleteHoroscopeItem(int id);
    }
}
