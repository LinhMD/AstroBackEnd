using AstroBackEnd.Models;

namespace AstroBackEnd.RequestModels.ZodiacHouseRequest
{
    public class FindZodiacHouse
    {
        public int? Id { get; set; }
        public int? ZodiacId { get; set; }
        public int? HouseId { get; set; }
        //public House House { get; set; }
        //public Zodiac Zodiac{ get; set; }
        public string Content { get; set; }
        public PagingRequest PagingRequest { get; set; }
    }
}
