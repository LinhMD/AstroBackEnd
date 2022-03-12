using AstroBackEnd.Models;

namespace AstroBackEnd.ViewsModel
{
    public class HoroscopeItemView
    {
        public HoroscopeItemView(HoroscopeItem horoscopeItem)
        {
            Id = horoscopeItem.Id;
            AspectId = horoscopeItem.AspectId;
            LifeAttributeId = horoscopeItem.LifeAttributeId;
            LifeAttributeName = horoscopeItem.LifeAttribute.Name;
            Value = horoscopeItem.Value;
        }
        public int Id { get; set; }

        public int AspectId { get; set; }

        //public Aspect Aspect { get; set; }

        public int LifeAttributeId { get; set; }

        public string LifeAttributeName { get; set; }

        public int Value { get; set; }

    }
}
