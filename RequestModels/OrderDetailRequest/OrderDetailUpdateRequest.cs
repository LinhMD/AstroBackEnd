using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.OrderDetailRequest
{
    public class OrderDetailUpdateRequest
    {

        public int? Quantity { get; set; }

        public string? ReviewMessage { get; set; }

        public DateTime? ReviewDate { get; set; }
    }
}
