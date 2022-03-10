using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace AstroBackEnd.RequestModels.NewRequest
{
    public class NewsCreateRequest
    {
        [MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }

        public string Banner { get; set; }

        public string Tag { get; set; }

        public string HtmlContent { get; set; }
    }
}
