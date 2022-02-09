using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class BirthChart
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public Profile Profile { get; set; }

        public int ProfileId { get; set; }

    }
}
