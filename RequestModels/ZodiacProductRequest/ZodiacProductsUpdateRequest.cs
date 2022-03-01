using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.ZodiacProductRequest
{
    public class ZodiacProductsUpdateRequest
    {
        //public int Id { get; set; }

        //public Product Product { get; set; }
        public int ProductId { get; set; }

        //public Zodiac Zodiac { get; set; }
        public int ZodiacId { get; set; }
    }
}
