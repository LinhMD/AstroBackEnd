using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.TopicRequest;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Core
{
    public interface ITopicService
    { 
        public Topic CreateTopic(CreateTopicRequest request);
        public IEnumerable<Topic> FindTopic(FindTopicRequest request, out int total);
        public Topic GetTopic(int id);
        public Topic UpdateTopic(int id, UpdateTopicRequest request);
        public Topic DeleteTopic(int id);
    }
}
