using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class BirthChart
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string ImgLink { get; set; }

        [ForeignKey("Id")]
        public int ProfileId { get; set; }

    }
}
