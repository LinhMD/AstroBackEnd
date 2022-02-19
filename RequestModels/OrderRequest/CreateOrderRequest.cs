using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.OrderRequest
{
    public class CreateOrderRequest
    {
        public DateTime? OrderTime { get; set; }

        public string? DeliveryAdress { get; set; }

        public string? DeleveryPhone { get; set; }

        [Required]
        public int UserId { get; set; }

        private int _status = 0;

        public int? Status { get { return _status; } set { _status = value == null? 0 : (int)value; } }
    }
}
