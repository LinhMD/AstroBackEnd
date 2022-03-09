namespace AstroBackEnd.RequestModels.PlanetRequest
{
    public class FindPlanetRequest
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public string Tag { get; set; }

        public PagingRequest PagingRequest { get; set; }
    }
}
