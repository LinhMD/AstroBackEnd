using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.HoroscopeItemRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AstroBackEnd.Services.Implement
{
    public class HoroscopeItemService : IHoroscopeItemService, IDisposable
    {
        private readonly IUnitOfWork _work;
        public HoroscopeItemService(IUnitOfWork _work)
        {
            this._work = _work;
        }
        public HoroscopeItem CreateHoroscopeItem(CreateHoroscopeItemRequest request)
        {
            try
            {
                LifeAttribute checkLifeAttribute = _work.LifeAttributes.Get(request.LifeAttributeId);
                Aspect checkAspect = _work.Aspects.Get(request.AspectId);
                if (checkLifeAttribute == null)
                {
                    throw new ArgumentException("LifeAttribute not found ");
                }
                if (checkAspect == null)
                {
                    throw new ArgumentException("Aspect not found");
                }
                HoroscopeItem horoscopeItem = new HoroscopeItem()
                {
                    AspectId = request.AspectId,
                    LifeAttributeId = request.LifeAttributeId,
                    Value = request.Value,
                    Content = request.Content,
                };
                return _work.HoroscopeItems.Add(horoscopeItem);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("HoroscopeItemService : CreateHoroscopeItem : " + ex.Message);
            }
        }
        public HoroscopeItem GetHoroscopeItem(int id)
        { 
            try
            {
                Validation.ValidNumberThanZero(id, "Id must be than zero");
                HoroscopeItem checkHoroscopeItem = _work.HoroscopeItems.Get(id);
                if (checkHoroscopeItem != null)
                {
                    return _work.HoroscopeItems.GetHoroscopeItemWithAllData(id);
                }
                else { throw new ArgumentException("This HoroscopeItem not found"); }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("HoroscopeItemService : GetHoroscopeItem : " + ex.Message);
            }
        }

        public IEnumerable<HoroscopeItem> FindHoroscopeItem(FindHoroscopeItemRequest request, out int total)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                /*Func<HoroscopeItem, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkAspectId = true;
                    bool checkLifeAttributeId = true;
                    bool checkValue = true;
                    if (request.Id > 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (request.AspectId > 0)
                    {
                        checkAspectId = p.AspectId == request.AspectId;
                    }
                    if (request.LifeAttributeId > 0)
                    {
                        checkLifeAttributeId = p.LifeAttributeId == request.LifeAttributeId;
                    }
                    if (request.Value > 0)
                    {
                        checkValue = p.LifeAttributeId == request.LifeAttributeId;
                    }
                    return checkId && checkLifeAttributeId && checkAspectId && checkValue;
                };*/
                Expression<Func<HoroscopeItem, bool>> filter = a => (request.Id <= 0 || a.Id == request.Id) &&
                                                                    (request.AspectId <= 0 || a.AspectId == request.AspectId) &&
                                                                    (request.LifeAttributeId <= 0 || a.LifeAttributeId == request.LifeAttributeId) &&
                                                                    (request.Value <= 0 || a.Value == request.Value);
                PagingRequest pagingRequest = request.PagingRequest;
                Validation.ValidNumberThanZero(pagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(pagingRequest.PageSize, "PageSize must be than zero");
                if (pagingRequest != null)
                {
                    switch (pagingRequest.SortBy)
                    {
                        case "AspectId":
                            return _work.HoroscopeItems.FindAtDBPaging(filter, p => p.AspectId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "LifeAttributeId":
                            return _work.HoroscopeItems.FindAtDBPaging(filter, p => p.LifeAttributeId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "Value":
                            return _work.HoroscopeItems.FindAtDBPaging(filter, p => p.Value, out total, pagingRequest.Page, pagingRequest.PageSize);
                        default:
                            return _work.HoroscopeItems.FindAtDBPaging(filter, p => p.Id, out total, pagingRequest.Page, pagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<HoroscopeItem> result = _work.HoroscopeItems.FindAtDBPaging(filter, p => p.Id, out total);
                    total = result.Count();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("HoroscopeItemService : FindHoroscopeItem : " + ex.Message);
            }
        }

        public HoroscopeItem UpdateHoroscopeItem(int id, UpdateHoroscopeItemRequest request)
        {
            try
            {
                Validation.ValidNumberThanZero(id, "Id must be than zero");
                LifeAttribute checkLifeAttribute = _work.LifeAttributes.Get(request.LifeAttributeId);
                Aspect checkAspect = _work.Aspects.Get(request.AspectId);
                if (checkLifeAttribute == null)
                {
                    throw new ArgumentException("LifeAttribute not found ");
                }
                if (checkAspect == null)
                {
                    throw new ArgumentException("Aspect not found");
                }
                HoroscopeItem horoscopeItem = _work.HoroscopeItems.Get(id);
                if (horoscopeItem != null)
                {
                    if (request.AspectId > 0)
                    {
                        horoscopeItem.AspectId = request.AspectId;
                    }
                    if (request.LifeAttributeId > 0)
                    {
                        horoscopeItem.LifeAttributeId = request.LifeAttributeId;
                    }
                    if (request.Value > 0)
                    {
                        horoscopeItem.Value = request.Value;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Content))
                    {
                        horoscopeItem.Content = request.Content;
                    }
                    return horoscopeItem;
                }
                else
                {
                    throw new ArgumentException("This HoroscopeItem not found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("HoroscopeItemService : UpdateHoroscopeItem : " + ex.Message);
            }
        }

        public HoroscopeItem DeleteHoroscopeItem(int id)
        {
            try
            {
                Validation.ValidNumberThanZero(id, "Id must be than zero");
                HoroscopeItem horoscopeItem = _work.HoroscopeItems.Get(id);
                if (horoscopeItem != null)
                {
                    _work.HoroscopeItems.Remove(horoscopeItem);
                    return horoscopeItem;
                }
                else { throw new ArgumentException("This HoroscopeItem not found"); }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("HoroscopeItemService : DeleteHoroscopeItem : " + ex.Message);
            }
        }

        public void Dispose()
        {
            _work.Complete();
        }
    }
}
