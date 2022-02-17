using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels
{
    public class PagingRequest
    {
        public string? SortBy { get; set; }

        public int Page { get { return _page; } set { _page = value; } }

        private int _page = 1;

        public int PageSize { get { return _pageSize; } set { _pageSize = value; } }

        private int _pageSize = 20;
    }
}
