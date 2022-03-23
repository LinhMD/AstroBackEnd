using AstroBackEnd.Models;

namespace AstroBackEnd.ViewsModel
{
    public class PlanetZodiacView
    {
        public PlanetZodiacView(PlanetZodiac pl)
        {
            Id = pl.Id;
            ZodiacName = pl.Zodiac.Name;
            ZodiacId = pl.ZodiacId;
            PlanetName = pl.Planet.Name;
            PlanetId = pl.PlanetId;
            Content = pl.Content;
        }
        public int Id { get; set; }

        public string ZodiacName { get; set; }

        public int ZodiacId { get; set; }

        public string PlanetName { get; set; }

        public int PlanetId { get; set; }

        public string Content { get; set; }
    }
}
