using AstroBackEnd.Models;

namespace AstroBackEnd.RequestModels.ZodiacHouseRequest
{
    public class FindZodiacHouse
    {
        public int? Id { get; set; }
        public int? ZodiacId { get; set; }
        public int? HouseId { get; set; }
        public PagingRequest PagingRequest { get; set; }
    }
}
