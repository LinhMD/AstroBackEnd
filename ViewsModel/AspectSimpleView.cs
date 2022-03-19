using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.ViewsModel
{
    public class AspectSimpleView
    {

        public static readonly Dictionary<int, (string angleName, int angle)> AngleNames = new()
        {
            { 0, ("Conjunction", 0) },
            { 1, ("SemiSextile", 30) },
            { 2, ("SemiSquare", 45) },
            { 3, ("Sextile", 60) },
            { 4, ("Quintile", 72) },
            { 5, ("Square", 90) },
            { 6, ("Trine", 120) },
            { 7, ("Sesquiquadrate", 135) },
            { 8, ("BiQuintile", 144) },
            { 9, ("Quincunx", 150) },
            { 10, ("Opposition", 180) },
        };

        public AspectSimpleView(Aspect aspect)
        {
            Id = aspect.Id;
            PlanetBaseId = aspect.PlanetBaseId;
            if (aspect.PlanetBase != null)
                PlanetBaseName = aspect.PlanetBase.Name;

            PlanetCompareId = aspect.PlanetCompareId;
            if (aspect.PlanetCompare != null)
                PlanetCompareName = aspect.PlanetCompare.Name;

            (string angleName, int angle) = AngleNames[aspect.AngleType];
            AngleName = angleName;
            Angle = angle;

        }
        public int Id { get; set; }

        public string PlanetBaseName { get; set; }

        public int PlanetBaseId { get; set; }

        public string PlanetCompareName { get; set; }

        public int PlanetCompareId { get; set; }


        public string AngleName { get; set; }

        public int Angle { get; set; }
    }
}
