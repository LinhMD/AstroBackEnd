using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int Status { get; set; }

        public DateTime? OrderTime { get; set; }

        public double? TotalCost { get; set; }

        public string? DeliveryAdress { get; set; }

        public string? DeleveryPhone { get; set; }

        public IList<OrderDetail> OrderDetails { get; set; }
        public int UserId { get; set; }
    }
}
