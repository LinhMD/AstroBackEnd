using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels.TopicRequest;
using AstroBackEnd.Services.Core;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Implement
{
    public class TopicService : ITopicService, IDisposable
    {
        private readonly IUnitOfWork _work;
        public TopicService(IUnitOfWork _work)
        {
            this._work = _work;
        }

        public Topic CreateTopic(CreateTopicRequest createTopicRequest)
        {
            throw new NotImplementedException();
        }
        public Topic GetTopic(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> FindTopic(FindTopicRequest findTopicRequest, out int total)
        {
            throw new NotImplementedException();
        }

        public Topic UpdateTopic(int id, UpdateTopicRequest updateTopicRequest)
        {
            throw new NotImplementedException();
        }

        public Topic DeleteTopic(int id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
