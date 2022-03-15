using System.ComponentModel.DataAnnotations.Schema;

namespace AstroBackEnd.Models
{
    public class Quote
    {
        public int Id { get; set; }

        public string Content { get; set; }

        [ForeignKey("Zodiac")]
        public int ZodiacId { get; set; }
    }
}