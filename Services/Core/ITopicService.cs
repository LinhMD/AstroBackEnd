using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.TopicRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface ITopicService
    { 
        public Topic CreateTopic(CreateTopicRequest createTopicRequest);
        public IEnumerable<Topic> FindTopic(FindTopicRequest findTopicRequest, out int total);
        public Topic GetTopic(int id);
        public Topic UpdateTopic(int id, UpdateTopicRequest updateTopicRequest);
        public Topic DeleteTopic(int id);
    }
}
