using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.ViewsModel
{
    public class ChartItemView
    {
        public ChartItemView(PlanetZodiac pZodiac, PlanetHouse pHouse, Planet planet)
        {
            if (pHouse.PlanetId != planet.Id || pZodiac.PlanetId != planet.Id) throw new ArgumentException("Planet id not equal");

            PlanetId = planet.Id;
            PlanetName = planet.Name;
            PlanetIcon = planet.Icon;

            ZodiacId = pZodiac.ZodiacId;
            ZodiacName = pZodiac.Zodiac.Name;
            ZodiacIcon = pZodiac.Zodiac.Icon;

            HouseId = pHouse.HouseId;
            HouseName = pHouse.House.Name;
            HouseIcon = pHouse.House.Icon;

            
            Content += pZodiac.Content;

            Content += "\n";
            
            Content += pHouse.Content;
        }
        public int PlanetId { get; set; }

        public string PlanetName { get; set; }

        public string PlanetIcon { get; set; }

        public int ZodiacId { get; set; }

        public string ZodiacName { get; set; }

        public string ZodiacIcon { get; set; }

        public int HouseId { get; set; }

        public string HouseName { get; set; }

        public string HouseIcon { get; set; }

        public string Content { get; set; }

         
    }
}
