using AstroBackEnd.Models;

namespace AstroBackEnd.RequestModels.PlanetZodiacRequest
{
    public class UpdatePlanetZodiacRequest
    {
        public int ZodiacId { get; set; }
        public int PlanetId { get; set; }
        public string Content { get; set; }
    }
}
