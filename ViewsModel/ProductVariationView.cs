using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.ViewsModel
{
    public class ProductVariationView
    {
        public ProductVariationView()
        {

        }
        public ProductVariationView(Product p)
        {
            Id = p.Id;
            MasterId = p.Id;
            Color = p.Color;
            Gender = p.Gender;
            Inventory = p.Inventory;
            Price = p.Price;
            Size = p.Size;
            ImgLinks = p.ImgLinks?.Select(i => i.Link).ToArray();
            Status = p.Status;
        }

        public int Id { get; set; }

        public int MasterId { get; set; }

        public string Size { get; set; }

        public double? Price { get; set; }

        public int? Gender { get; set; }

        public string Color { get; set; }

        public int? Inventory { get; set; }


        //both will have imgLink
        public IList<string> ImgLinks { get; set; }

        public int Status { get; set; }
    }
}
