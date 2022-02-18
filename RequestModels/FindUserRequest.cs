using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels
{
    public class FindUserRequest
    {

        public string? Name { get; set; }

        public string? Phone { get; set; }

        public int Status { get { return _status; } set { _status = value; } }

        private int _status = 1;

        public PagingRequest PagingRequest { get; set; }


    }
}
