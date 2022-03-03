namespace AstroBackEnd.RequestModels.HouseRequest
{
    public class UpdateHouseRequest
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string MainContent { get; set; }
    }
}
