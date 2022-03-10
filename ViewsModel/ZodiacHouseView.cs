using AstroBackEnd.Models;

namespace AstroBackEnd.ViewsModel
{
    public class ZodiacHouseView
    {

        public ZodiacHouseView(ZodiacHouse zodiacHouse)
        {
            Id = zodiacHouse.Id;
            ZodiacId = zodiacHouse.ZodiacId;
            ZodiacName = zodiacHouse.Zodiac.Name;
            HouseId = zodiacHouse.HouseId;
            HouseName = zodiacHouse.House.Name;
            Content = zodiacHouse.Content;
        }
        public int Id { get; set; }

        public string ZodiacName { get; set; }

        public int ZodiacId { get; set; }

        public string HouseName { get; set; }

        public int HouseId { get; set; }

        public string Content { get; set; }
    }
}
