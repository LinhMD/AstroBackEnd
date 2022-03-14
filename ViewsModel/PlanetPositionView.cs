using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.ViewsModel
{
    public class PlanetPositionView
    {
        public int PlanetId { get; set; }

        public int HouseId { get; set; }

        public string HouseName { get; set; }

        public int ZodiacId { get; set; }

        public string ZodiacName { get; set; }
         
    }
}
