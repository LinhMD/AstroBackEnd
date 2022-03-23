using AstroBackEnd.Models;

namespace AstroBackEnd.ViewsModel
{
    public class HoroscopeItemView
    {
        public HoroscopeItemView(HoroscopeItem horoscopeItem)
        {
            Id = horoscopeItem.Id;
            Aspect = new AspectView(horoscopeItem.Aspect);
            LifeAttributeId = horoscopeItem.LifeAttributeId;
            LifeAttributeName = horoscopeItem.LifeAttribute.Name;
            Value = horoscopeItem.Value;
            Content = horoscopeItem.Content;
        }

        public int Id { get; set; }

        public AspectView Aspect { get; set; }

        public int LifeAttributeId { get; set; }

        public string LifeAttributeName { get; set; }

        public int Value { get; set; }

        public string Content { get; set; }

    }
}
