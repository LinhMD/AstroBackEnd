using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.OrderDetailRequest
{
    public class OrderDetailCreateRequest
    {
        public int OrderId { get; set; }

        [Range(0, int.MaxValue)]
        public int ProductId { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

    }
}
