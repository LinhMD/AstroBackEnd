using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class Horoscope
    {
        public int Id { get; set; }

        public string ColorLuck { get; set; }

        public float NumberLuck { get; set; }

        public string Work { get; set; }

        public string Love { get; set; }

        public string Money { get; set; }

        public IList<Quote> Quotes { get; set; }

        public IList<Zodiac> Zodiacs { get; set; }

    }
}
