using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.OrderRequest
{
    public class FindOrderRequest
    {

        public int? Status { get; set; }

        public DateTime? OrderTimeStart { get; set; }

        public DateTime? OrderTimeEnd { get; set; }

        public double? TotalCostStart { get; set; }

        public double? TotalCostEnd { get; set; }

        public string? DeliveryAdress { get; set; }

        public string? DeleveryPhone { get; set; }

        public int? UserId { get; set; }

        public PagingRequest? PagingRequest { get; set; }
    }
}
