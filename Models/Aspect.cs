using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class Aspect
    {

        public int Id { get; set; }

        public Planet PlanetBase { get; set; }

        public int PlanetBaseId { get; set; }

        public Planet PlanetCompare { get; set; }

        public int PlanetCompareId { get; set; }

        [Range(0, 6, ErrorMessage = "Angle type [0-6]")]
        public int AngleType { get; set; }

        public string? Description { get; set; }

        public string? MainContent { get; set; }
    }
}
