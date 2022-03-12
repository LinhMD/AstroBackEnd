using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels.LifeAttributeRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Services.Implement
{
    public class LifeAttributeService : ILifeAttributeService, IDisposable
    {
        private readonly IUnitOfWork _work;
        public LifeAttributeService(IUnitOfWork _work)
        {
            this._work = _work;
        }
        public LifeAttribute CreateLifeAttribute(CreateLifeAttributeRequest request)
        {
            try
            {
                LifeAttribute lifeAttribute = new LifeAttribute()
                {
                    Name = request.Name,
                };
                return _work.LifeAttributes.Add(lifeAttribute);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("LifeAttributeService : CreateLifeAttribute : " + ex.Message);
            }
        }

        public LifeAttribute GetLifeAttribute(int id)
        {
            try
            {
                Validation.ValidNumberThanZero(id, "Id must be than zero");
                LifeAttribute checkLifeAttribute = _work.LifeAttributes.Get(id);
                if (checkLifeAttribute != null)
                {
                    return _work.LifeAttributes.Get(id);
                }
                else { throw new ArgumentException("This LifeAttribute not found"); }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("LifeAttributeService : GetLifeAttribute : " + ex.Message);
            }
        }

        public IEnumerable<LifeAttribute> FindLifeAttribute(FindLifeAttributeRequest request, out int total)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<LifeAttribute, bool> filter = p =>
                {
                    bool checkName = true;
                    bool checkId = true;
                    if (request.Id > 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Name))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Name))
                        {
                            checkName = p.Name.ToLower().Contains(request.Name.ToLower());
                        }
                        else
                        {
                            checkName = false;
                        }
                    }
                    return checkName && checkId;
                };
                Validation.ValidNumberThanZero(request.PagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(request.PagingRequest.PageSize, "PageSize must be than zero");
                if (request.PagingRequest != null)
                {
                    switch (request.PagingRequest.SortBy)
                    {
                        case "Name":
                            return _work.LifeAttributes.FindPaging(filter, p => p.Name, out total, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        default:
                            return _work.LifeAttributes.FindPaging(filter, p => p.Id, out total, request.PagingRequest.Page, request.PagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<LifeAttribute> result = _work.LifeAttributes.Find(filter, p => p.Id);
                    total = result.Count();
                    return result;
                }
            }
            catch (Exception ex) { throw new ArgumentException("LifeAttributeService : FindLifeAttribute : " + ex.Message); }
        }

        public LifeAttribute UpdateLifeAttribute(int id, UpdateLifeAttributeRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            LifeAttribute lifeAttribute = _work.LifeAttributes.Get(id);
            if (lifeAttribute != null)
            {
                if (request.Name != null)
                {
                    lifeAttribute.Name = request.Name;
                }
                return lifeAttribute;
            }
            else
            {
                throw new ArgumentException("This LifeAttribute not found");
            }
        }

        public LifeAttribute DeleteLifeAttribute(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            LifeAttribute lifeAttribute = _work.LifeAttributes.Get(id);
            if (lifeAttribute != null)
            {
                _work.LifeAttributes.Remove(lifeAttribute);
                return lifeAttribute;
            }
            else { throw new ArgumentException("This LifeAttribute not found"); }
        }

        public void Dispose()
        {
            _work.Complete();
        }
    }
}
