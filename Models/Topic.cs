using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class Topic
    {
        public int Id { get; set; }

        public int Name { get; set; }

        [ForeignKey("Zodiac")]
        public int ZodiacId { get; set; }
    }
}
