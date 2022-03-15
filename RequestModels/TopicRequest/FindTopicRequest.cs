namespace AstroBackEnd.RequestModels.TopicRequest
{
    public class FindTopicRequest
    {
        public int Id { get; set; }

        public int Name { get; set; }

        public int HouseId { get; set; }

        public PagingRequest PagingRequest { get; set; }
    }
}
