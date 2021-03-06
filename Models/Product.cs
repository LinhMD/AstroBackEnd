using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

       //product master part

        [MaxLength(512)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Detail { get; set; }

        public string? Tag { get; set; }

        public Category Category { get; set; }

        public int CategoryId { get; set; }

        public IList<Product> ProductVariation { get; set; }


        //product variation part

        public Product? MasterProduct { get; set; }

        public int? MasterProductId { get; set; }

        [MaxLength(255)]
        public string? Size { get; set; }

        [Range(0d, double.MaxValue, ErrorMessage = "Price must be positive")]
        public double? Price { get; set; }

        public int? Gender { get; set; }

        [MaxLength(255)]
        public string? Color { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Inventory must be positive")]
        public int? Inventory { get; set; }


        //both will have imgLink
        public IList<ImgLink> ImgLinks { get; set; }

       public int Status { get; set; }
    }
}
