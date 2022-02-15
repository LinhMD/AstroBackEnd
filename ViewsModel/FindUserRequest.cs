using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.ViewsModel
{
    public class FindUserRequest
    {

        public string Name { get; set; }

        public string Phone { get; set; }

        public int Status { get { return _status; } set { _status = value; } }

        private int _status = 1;

        public string SortBy { get; set; }

        public int Page { get { return _page; } set { _page = value; } }

        private int _page = 1;

        public int PageSize { get { return _pageSize; } set { _pageSize = value; } }

        private int _pageSize = 20;


    }
}
