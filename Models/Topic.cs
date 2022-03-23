using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class Topic
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int HouseId { get; set; }
    }
}
