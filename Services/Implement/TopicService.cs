using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.TopicRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Services.Implement
{
    public class TopicService : ITopicService, IDisposable
    {
        private readonly IUnitOfWork _work;
        public TopicService(IUnitOfWork _work)
        {
            this._work = _work;
        }

        public Topic CreateTopic(CreateTopicRequest request)
        {
            try
            {
                
                House checkHouse = _work.Houses.Get(request.HouseId);
                if (checkHouse == null)
                {
                    throw new ArgumentException("House not exist");
                }
                IEnumerable<Topic> result = _work.Topics.FindPaging(t => t.Name == request.Name && t.HouseId == request.HouseId, p => p.Id);
                if (result.Count() > 0)
                {
                    throw new ArgumentException("Topic already exist");
                }
                Topic topic = new Topic()
                {
                    Name = request.Name,
                    HouseId = request.HouseId
                };
                return _work.Topics.Add(topic);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("TopicService : " + ex.Message);
            }
        }
        public Topic GetTopic(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Topic checkTopic = _work.Topics.Get(id);
            if (checkTopic != null)
            {
                return _work.Topics.Get(id);
            }
            else { throw new ArgumentException("This Topic not found"); }
        }

        public IEnumerable<Topic> FindTopic(FindTopicRequest request, out int total)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<Topic, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkName = true;
                    bool checkHouseId = true;
                    if (request.Id > 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Name))
                    {
                        checkName = p.Name.Contains(request.Name);

                    }
                    if (request.HouseId > 0)
                    {
                        checkHouseId = p.HouseId == request.HouseId;
                    }
                    return checkHouseId && checkId && checkName ;
                };
                PagingRequest pagingRequest = request.PagingRequest;
                Validation.ValidNumberThanZero(pagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(pagingRequest.PageSize, "PageSize must be than zero");
                if (pagingRequest != null)
                {
                    switch (pagingRequest.SortBy)
                    {
                        case "Id":
                            return _work.Topics.FindPaging(filter, p => p.Id, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "Name":
                            return _work.Topics.FindPaging(filter, p => p.Name, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "HouseId":
                            return _work.Topics.FindPaging(filter, p => p.HouseId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        default:
                            return _work.Topics.FindPaging(filter, p => p.Id, out total, pagingRequest.Page, pagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<Topic> result = _work.Topics.FindPaging(filter, p => p.Id, out total);
                    return result;
                }
            }
            catch (Exception ex) { throw new ArgumentException("TopicService : " + ex.Message); }
        }

        public Topic UpdateTopic(int id, UpdateTopicRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            IEnumerable<Topic> result = _work.Topics.FindPaging(t => t.Name == request.Name && t.HouseId == request.HouseId, p => p.Id);
            if (result.Any())
            {
                throw new ArgumentException("Topic already exist");
            }

            Topic topic = _work.Topics.Get(id);
            if (topic != null)
            {
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    topic.Name = request.Name;
                }
                if (request.HouseId > 0)
                {
                    topic.HouseId = request.HouseId;
                }
                return topic;
            }
            else
            {
                throw new ArgumentException("This topic not found");
            }
        }

        public Topic DeleteTopic(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Topic topic = _work.Topics.Get(id);
            if (topic != null)
            {
                _work.Topics.Remove(topic);
                return topic;
            }
            else { throw new ArgumentException("This topic not found"); }
        }

        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
