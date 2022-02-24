namespace AstroBackEnd.RequestModels
{
    public class UpdateZodiacRequest
    {
        public string _zodiacName;
        public int _zodiacDayStart;
        public int _zodiacMonthStart;
        public int _zodiacDayEnd;
        public int _zodiacMonthEnd;
        public string _zodiacIcon;
        public string _zodiacDescription;
        public string _zodiacMainContent;


        public string ZodiacName { get => _zodiacName; set => _zodiacName = value; }
        public int ZodiacDayStart { get => _zodiacDayStart; set => _zodiacDayStart = value; }
        public int ZodiacMonthStart { get => _zodiacMonthStart; set => _zodiacMonthStart = value; }
        public int ZodiacDayEnd { get => _zodiacDayEnd; set => _zodiacDayEnd = value; }
        public int ZodiacMonthEnd { get => _zodiacMonthEnd; set => _zodiacMonthEnd = value; }
        public string ZodiacIcon { get => _zodiacIcon; set => _zodiacIcon = value; }
        public string ZodiacDescription { get => _zodiacDescription; set => _zodiacDescription = value; }
        public string ZodiacMainContent { get => _zodiacMainContent; set => _zodiacMainContent = value; }
    }
}
