using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.NewRequest
{
    public class FindNewsRequest
    {
        
        public string? Title { get; set; }

        public string? Description { get; set; }

        public string? Tag { get; set; }

        public string? SortBy { get; set; }

        public int Page { get { return _page; } set { _page = value; } }

        private int _page = 1;

        public int PageSize { get { return _pageSize; } set { _pageSize = value; } }

        private int _pageSize = 20;
    }
}
