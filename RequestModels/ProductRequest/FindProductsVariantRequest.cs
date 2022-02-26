using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.ProductRequest
{
    public class FindProductsVariantRequest
    {

        public string? Size { get; set; }

        public double? Price { get; set; }

        public int? Gender { get; set; }

        public string? Color { get; set; }

        public PagingRequest? PagingRequest { get; set; }
    }
}
