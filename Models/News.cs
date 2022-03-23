using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class News
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Banner { get; set; }

        public DateTime GeneratDate { get; set; }

        public string Content { get; set; }

        public string Tag { get; set; }

        public string HtmlContent { get; set; }
    }
}
