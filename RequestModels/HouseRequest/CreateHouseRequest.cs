namespace AstroBackEnd.RequestModels.HouseRequest
{
    public class CreateHouseRequest
    {
        private string name;
        private string title;
        private string icon;
        private string description;
        private string tag;
        private string mainContentl;

        public string Name { get => name; set => name = value; }
        public string Title { get => title; set => title = value; }
        public string Icon { get => icon; set => icon = value; }
        public string Description { get => description; set => description = value; }
        public string Tag { get => tag; set => tag = value; }
        public string MainContentl { get => mainContentl; set => mainContentl = value; }
    }
}
