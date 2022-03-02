using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.OrderDetailRequest
{
    public class OrderDetailUpdateRequest
    {
        [Range(0, int.MaxValue)]
        public int? Quantity { get; set; }

        public string? ReviewMessage { get; set; }

        public DateTime? ReviewDate { get; set; }
    }
}
