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
            this.Description = product.Description;
            this.Detail = product.Detail;
            this.CategoryId = product.Category.Id;
            this.CategoryName = product.Category.Name;
            this.ImgLinks = product.ImgLinks.Select(i => i.Link).ToArray();
            this.ZodiacNames = product.Zodiacs.Select(z => z.Name).ToArray();
            this.ProductVariation = product.ProductVariation.Select(p => new ProductVariationView(p)).ToArray();
            this.Status = product.Status;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Detail { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        
        public IList<string> ImgLinks { get; set; }

        public IList<string> ZodiacNames { get; set; }

        public IList<ProductVariationView> ProductVariation { get; set; }

        public int Status { get; set; }
    }
}
