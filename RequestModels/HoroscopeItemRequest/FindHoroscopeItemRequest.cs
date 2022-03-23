namespace AstroBackEnd.RequestModels.HoroscopeItemRequest
{
    public class FindHoroscopeItemRequest
    {
        public int Id { get; set; }

        public int AspectId { get; set; }

        public int LifeAttributeId { get; set; }

        public int Value { get; set; }

        public PagingRequest PagingRequest { get; set; }

    }
}
