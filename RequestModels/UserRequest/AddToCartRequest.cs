using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.UserRequest
{
    public class AddToCartRequest
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, int.MaxValue)]
        public int Quantity { get { return quantity; } set { quantity = value; } }

        [Range(1, int.MaxValue)]
        private int quantity = 1;
    }
}
