using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.OrderDetailRequest
{
    public class FindOrderDetailRequest
    {

        public int? OrderId { get; set; }

        public string? ProductName { get; set; }

        public double? TotalPriceStart { get; set; }

        public double? TotalPriceEnd { get; set; }

        public int? QuantityStart { get; set; }

        public int? QuantityEnd { get; set; }

        public string? ReviewMessage { get; set; }

        public DateTime? ReviewDateStart { get; set; }

        public DateTime? ReviewDateEnd { get; set; }

        public PagingRequest? Paging { get; set; }
    }
}
