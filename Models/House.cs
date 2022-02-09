using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class House
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public string Decription { get; set; }

        public string? Tag { get; set; }

        public string MainContent { get; set; }

    }
}
