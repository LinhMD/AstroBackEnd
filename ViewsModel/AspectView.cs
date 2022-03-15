using AstroBackEnd.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace AstroBackEnd.ViewsModel
{
    public class AspectView
    {
        public AspectView(Aspect aspect)
        {
            Id = aspect.Id;
            PlanetBaseId = aspect.PlanetBaseId;
            PlanetBaseName = aspect.PlanetBase.Name;
            PlanetCompareId = aspect.PlanetCompareId;
            PlanetCompareName = aspect.PlanetCompare.Name;
            AngleType = aspect.AngleType;
        }

        public int Id { get; set; }

        public string PlanetBaseName { get; set; }

        public int PlanetBaseId { get; set; }

        public string PlanetCompareName { get; set; }

        public int PlanetCompareId { get; set; }

        [Range(0, 6, ErrorMessage = "Angle type [0-6]")]
        public int AngleType { get; set; }
    }
}
