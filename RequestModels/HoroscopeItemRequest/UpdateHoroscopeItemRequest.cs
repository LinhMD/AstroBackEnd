namespace AstroBackEnd.RequestModels.HoroscopeItemRequest
{
    public class UpdateHoroscopeItemRequest
    {
        public int AspectId { get; set; }

        public int LifeAttributeId { get; set; }

        public int Value { get; set; }

        public string Content { get; set; }
    }
}
