using System.ComponentModel.DataAnnotations;

namespace AstroBackEnd.RequestModels.AspectRequest
{
    public class FindAspectRequest
    {
        public int Id { get; set; }

        public int PlanetBaseId { get; set; }

        public int PlanetCompareId { get; set; }

        [Range(0, 6, ErrorMessage = "Angle type [0-6]")]
        public int AngleType { get; set; }

        public string? Description { get; set; }

        public string? MainContent { get; set; }

        public PagingRequest PagingRequest { get; set; }
    }
}
