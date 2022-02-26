using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.ProductRequest
{
    public class MasterProductsUpdateRequest
    {
        public int MasterProductId { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Detail { get; set; }

        public int CatagoryId { get; set; }

        public IList<string> ImgLinksAdd { get; set; }
    }
}
