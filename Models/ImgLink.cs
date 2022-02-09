using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class ImgLink
    {
        public int Id { get; set; }

        public string Link { get; set; }

        public int ProductId { get; set; }
    }
}
