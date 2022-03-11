using AstroBackEnd.Models;

namespace AstroBackEnd.ViewsModel
{
    public class PlanetView
    {
        public PlanetView(Planet planet)
        {
            Id = planet.Id;
            Name = planet.Name;
            Title = planet.Title;
            Icon = planet.Icon;
            Tag = planet.Tag;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public string? Tag { get; set; }
    }
}
