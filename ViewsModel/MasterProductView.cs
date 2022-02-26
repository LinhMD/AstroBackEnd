using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.ViewsModel
{
    public class MasterProductView 
    {
        public MasterProductView(Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            Description = product.Description;
            Detail = product.Detail;
            CatagoryId = product.Catagory.Id;
            CatagoryName = product.Catagory.Name;
            this.ImgLinks = product.ImgLinks.Select(i => i.Link).ToArray();
            this.ZodiacNames = product.Zodiacs.Select(z => z.Name).ToArray();
            this.ProductVariation = product.ProductVariation.Select(p => new ProductVariationView(p)).ToArray();
            
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Detail { get; set; }

        public int CatagoryId { get; set; }

        public string CatagoryName { get; set; }

        
        public IList<string> ImgLinks { get; set; }

        public IList<string> ZodiacNames { get; set; }

        public IList<ProductVariationView> ProductVariation { get; set; }
    }
}
