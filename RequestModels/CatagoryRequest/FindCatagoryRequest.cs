using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.CatagoryRequest
{
    public class FindCatagoryRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public PagingRequest? PagingRequest { get; set; }
    }
}
