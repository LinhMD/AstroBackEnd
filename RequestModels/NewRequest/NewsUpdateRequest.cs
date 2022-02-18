using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.NewRequest
{
    public class NewsUpdateRequest
    {
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Content { get; set; }

        public string? Tag { get; set; }

        public string? HtmlContent { get; set; }
    }
}
