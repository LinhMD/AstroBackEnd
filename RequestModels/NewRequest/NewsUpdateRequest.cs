using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.NewRequest
{
 
    /// <summary>
    /// News Update request Model
    /// </summary>
    public class NewsUpdateRequest
    {
        /// <summary>
        /// title Nà
        /// </summary>
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Content { get; set; }

        public string? Banner { get; set; }

        public string? Tag { get; set; }

        public string? HtmlContent { get; set; }

    }
}
