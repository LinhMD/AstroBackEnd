using AstroBackEnd.Models;

namespace AstroBackEnd.RequestModels.PlanetZodiacRequest
{
    public class FindPlanetZodiacRequest
    {
        public int Id { get; set; }
        public int ZodiacId { get; set; }
        public int PlanetId { get; set; }
        public PagingRequest PagingRequest { get; set; }
    }
}
