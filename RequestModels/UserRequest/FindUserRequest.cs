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

        public int? Status { get; set; }

        public PagingRequest PagingRequest { get; set; }


    }
}
