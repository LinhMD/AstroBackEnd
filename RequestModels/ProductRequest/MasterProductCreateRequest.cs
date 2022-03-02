using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.RequestModels.ProductRequest
{
    public class MasterProductCreateRequest
    {

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Detail { get; set; }

        [Required]
        public int CatagoryId { get; set; }

        public List<string> ImgLink { get; set; }
    }
}
