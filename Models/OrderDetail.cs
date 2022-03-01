using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public double TotalPrice { get; set; }

        public int Quantity { get; set; }

        public string? ReviewMessage { get; set; }

        public DateTime? ReviewDate { get; set; }
    }
}
