using System.Collections.Generic;

namespace AstroBackEnd.Models
{
    public class Zodiac
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ZodiacDayStart { get; set; }

        public int ZodiacMonthStart { get; set; }

        public int ZodiacDayEnd { get; set; }

        public int ZodiacMonthEnd { get; set; }

        public string Icon { get; set; }

        public string Descreiption { get; set; }

        public string MainContent { get; set; }

        public IList<Horoscope> Horoscopes { get; set; }

        public int MainHouse { get; set; }
    }
}