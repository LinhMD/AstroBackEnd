using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.HoroscopeRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface IHoroscopeService
    {
        public Horoscope CreateHoroscope(CreateHoroscopeRequest request);
        public IEnumerable<Horoscope> FindHoroscope(FindHoroscopeRequest request);
        public Horoscope GetHoroscope(int id);
        public Horoscope UpdateHoroscope(int id, UpdateHoroscopeRequest request);
        public Horoscope DeleteHoroscope(int id);

    }
}
