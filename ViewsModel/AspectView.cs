using AstroBackEnd.Models;
using System.ComponentModel.DataAnnotations;

namespace AstroBackEnd.ViewsModel
{
    public class AspectView
    {
        public AspectView(Aspect aspect)
        {
            Id = aspect.Id;
            PlanetBaseName = aspect.PlanetBase.Name;
            PlanetBaseId = aspect.PlanetBaseId;
            PlanetCompareName = aspect.PlanetCompare.Name;
            PlanetCompareId = aspect.PlanetCompareId;
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
