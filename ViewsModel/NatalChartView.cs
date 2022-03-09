using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.ViewsModel
{
    public class NatalChartView
    {

        public Dictionary<string, int> PlanetInHouse { get; set; }
        public Dictionary<string, string> PlanetInZodiac { get; set; }
        public Dictionary<string, int> ZodiacInHouse  { get; set; }

        public Zodiac zodiac { get; set; }
    }
}
