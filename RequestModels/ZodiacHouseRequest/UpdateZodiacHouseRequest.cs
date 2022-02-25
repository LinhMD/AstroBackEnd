namespace AstroBackEnd.RequestModels.ZodiacHouseRequest
{
    public class UpdateZodiacHouseRequest
    {
        public int ZodiacId { get; set; }
        public int HouseId { get; set; }
        public string Content { get; set; }
    }
}
