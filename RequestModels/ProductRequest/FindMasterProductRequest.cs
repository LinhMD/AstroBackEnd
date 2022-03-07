using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.ProductRequest
{
    public class FindMasterProductRequest
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? Detail { get; set; }

        public int? CategoryId { get; set; }

        public int? ZodiacsId{ get; set; }

        public int? ProductVariationId { get; set; }

        public int? Status { get; set; }

        public PagingRequest? PagingRequest { get; set; }

        
    }
}
