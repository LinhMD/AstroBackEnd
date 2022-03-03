namespace AstroBackEnd.RequestModels.HoroscopeRequest
{
    public class FindHoroscopeRequest
    {
        public int Id { get; set; }

        public string ColorLuck { get; set; }

        public float NumberLuck { get; set; }

        public string Work { get; set; }

        public string Love { get; set; }

        public string Money { get; set; }

        public PagingRequest PagingRequest { get; set; }
    }
}
