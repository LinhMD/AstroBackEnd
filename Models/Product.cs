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

        public Product MasterProduct { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Detail { get; set; }

        public Catagory Catagory { get; set; }

        public string? Size { get; set; }

        public double? Price { get; set; }

        public int? Gender { get; set; }

        public string? Color { get; set; }

        public int? Inventory { get; set; }


        public IList<ImgLink> ImgLinks { get; set; }

        public IList<Zodiac> Zodiacs { get; set; }

        public IList<Product> ProductVariation { get; set; }
    }
}
