using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class PlanetZodiac
    {
        public int Id { get; set; }

        public Zodiac Zodiac { get; set; }

        public int ZodiacId { get; set; }

        public Planet Planet { get; set; }

        public int PlanetId { get; set; }

        public string Content { get; set; }

    }
}
