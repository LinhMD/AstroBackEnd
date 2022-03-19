using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.ViewsModel
{
    public class HoroscopeItemSimpleView
    {
        public HoroscopeItemSimpleView(HoroscopeItem horoscopeItem)
        {
            Id = horoscopeItem.Id;
            Aspect = new AspectSimpleView(horoscopeItem.Aspect);
            LifeAttributeId = horoscopeItem.LifeAttributeId;
            LifeAttributeName = horoscopeItem.LifeAttribute.Name;
            Value = horoscopeItem.Value;
        }

        public int Id { get; set; }

        public AspectSimpleView Aspect { get; set; }

        public int LifeAttributeId { get; set; }

        public string LifeAttributeName { get; set; }

        public int Value { get; set; }

    }
}
