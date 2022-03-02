using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.ProductRequest
{
    public class ProductVariantCreateRequest
    {
        [Required(ErrorMessage = "Must have master product ID" )]
        public int MasterProductId { get; set; }

        [MaxLength(255)]
        public string? Size { get; set; }

        [Range(0d, double.MaxValue, ErrorMessage = "Price must be positive")]
        public double? Price { get; set; }


        public int? Gender { get; set; }

        [MaxLength(255)]
        public string? Color { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Inventory must be positive")]
        public int? Inventory { get; set; }

        public List<string> ImgLink { get; set; }
    }
}
