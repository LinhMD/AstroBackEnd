using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class PlanetHouse
    {
        public int Id { get; set; }

        public House House { get; set; }

        public int HouseId { get; set; }

        public Planet Planet { get; set; }

        public int PlanetId { get; set; }

        public string Content { get; set; }

    }
}
