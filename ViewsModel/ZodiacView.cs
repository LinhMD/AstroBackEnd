using AstroBackEnd.Models;

namespace AstroBackEnd.ViewsModel
{
    public class ZodiacView
    {
        public ZodiacView(Zodiac zodiac)
        {
            Id = zodiac.Id;
            Name = zodiac.Name;
            ZodiacDayStart = zodiac.ZodiacDayStart;
            ZodiacMonthStart = zodiac.ZodiacMonthStart;
            ZodiacDayEnd = zodiac.ZodiacDayEnd;
            ZodiacMonthEnd = zodiac.ZodiacMonthEnd;
            Icon = zodiac.Icon;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public int ZodiacDayStart { get; set; }

        public int ZodiacMonthStart { get; set; }

        public int ZodiacDayEnd { get; set; }

        public int ZodiacMonthEnd { get; set; }

        public string Icon { get; set; }

    }
}
