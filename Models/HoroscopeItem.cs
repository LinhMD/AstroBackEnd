using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Models
{
    public class HoroscopeItem
    {
        public int Id { get; set; }

        public int AspectId { get; set; }

        public Aspect Aspect { get; set; }

        public int LifeAttributeId { get; set; }

        public LifeAttribute LifeAttribute { get; set; }

        public int Value { get; set; }

        public string Content { get; set; }
    }
}
