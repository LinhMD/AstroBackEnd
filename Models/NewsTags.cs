using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class NewsTags
    {
        public int Id { get; set; }

        public Zodiac? Zodiac { get; set; }

        public int? ZodiacId { get; set; }

        public House? House { get; set; }

        public int? HouseId { get; set; }

        public Planet? Planet { get; set; }
    
        public int? PlanetId { get; set; }

        public News News { get; set; }

        public int NewId { get; set; }
    }
            
}
