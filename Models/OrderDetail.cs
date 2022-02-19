using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Product Product { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public string? ReviewMessage { get; set; }

        public DateTime? ReviewDate { get; set; }
    }
}
