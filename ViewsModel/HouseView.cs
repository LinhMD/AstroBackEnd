using AstroBackEnd.Models;

namespace AstroBackEnd.ViewsModel
{
    public class HouseView
    {
        public HouseView(House house)
        {
            Id = house.Id;
            Name = house.Name;
            Title = house.Title;
            Icon = house.Icon;
            Tag = house.Tag;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public string? Tag { get; set; }

    }
}
