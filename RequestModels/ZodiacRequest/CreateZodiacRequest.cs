using System.ComponentModel.DataAnnotations;

namespace AstroBackEnd.RequestModels
{
    public class CreateZodiacRequest
    {
        public string ZodiacName { get; set; }
        public int ZodiacDayStart { get; set; }
        public int ZodiacMonthStart { get; set; }
        public int ZodiacDayEnd { get; set; }
        public int ZodiacMonthEnd { get; set; }
        public string ZodiacIcon { get; set; }
        public string ZodiacDescription { get; set; }
        public string ZodiacMainContent { get; set; }
    }
}

