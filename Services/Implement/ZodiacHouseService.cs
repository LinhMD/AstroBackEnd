using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.ZodiacHouseRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Services.Implement
{
    public class ZodiacHouseService : IZodiacHouseService, IDisposable
    {
        IUnitOfWork _work;
        public ZodiacHouseService(IUnitOfWork _work)
        {
            this._work = _work;
        }

        public ZodiacHouse GetZodiacHouse(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            ZodiacHouse checkZodiacHouse = _work.ZodiacHouses.Get(id);
            if (checkZodiacHouse != null)
            {
                return _work.ZodiacHouses.GetZodiacHouseWithAllData(id);
            }
            else { throw new ArgumentException("This ZodiacHouse not found"); }
        }
        public ZodiacHouse CreateZodiacHouse(CreateZodiacHouseRequest request)
        {
            try
            {
                House checkHouse = _work.Houses.Get(request.HouseId);
                Zodiac checkZodiac = _work.Zodiacs.Get(request.ZodiacId);
                if (checkHouse == null)
                {
                    throw new ArgumentException("House not exist ");
                }
                if (checkZodiac == null)
                {
                    throw new ArgumentException("Zodiac not exist");
                }
                Func<ZodiacHouse, bool> filter = p =>
                {
                    bool checkZodiacId = true;
                    bool checkHouseId = true;
                    if (request.ZodiacId > 0)
                    {
                        checkZodiacId = p.ZodiacId == request.ZodiacId;
                    }
                    if (request.HouseId > 0)
                    {
                        checkHouseId = p.HouseId == request.HouseId;
                    }
                    return checkHouseId && checkZodiacId;
                };
                IEnumerable<ZodiacHouse> result = _work.ZodiacHouses.FindPaging(filter, p => p.Id);
                if (result.Count() > 0)
                {
                    throw new ArgumentException("ZodiacHouse exist");
                }
                ZodiacHouse zodiacHouse = new ZodiacHouse()
                {
                    HouseId = request.HouseId,
                    ZodiacId = request.ZodiacId,
                    Content = request.Content
                };
                return _work.ZodiacHouses.Add(zodiacHouse);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("ZodiacHouseService : " + ex.Message);
            }
        }

        public IEnumerable<ZodiacHouse>  FindZodiacHouse(FindZodiacHouse request, out int total)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<ZodiacHouse, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkZodiacId = true;
                    bool checkHouseId = true;
                    if (request.Id > 0)
                    {
                        checkId = request.Id == p.Id;
                    }
                    if (request.HouseId > 0)
                    {
                        checkHouseId = request.HouseId == p.HouseId;
                    }
                    if (request.ZodiacId > 0)
                    {
                        checkZodiacId = request.ZodiacId == p.ZodiacId;
                    }
                    return checkId && checkHouseId && checkZodiacId;
                };
                PagingRequest pagingRequest = request.PagingRequest;
                Validation.ValidNumberThanZero(pagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(pagingRequest.PageSize, "PageSize must be than zero");
                if (pagingRequest != null)
                {
                    switch (pagingRequest.SortBy)
                    {
                        case "ZodiacId":
                            return _work.ZodiacHouses.FindPPagingZodiacHouseWithAllData(filter, p => p.ZodiacId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "HouseId":
                            return _work.ZodiacHouses.FindPPagingZodiacHouseWithAllData(filter, p => p.HouseId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        default:
                            return _work.ZodiacHouses.FindPPagingZodiacHouseWithAllData(filter, p => p.Id, out total, pagingRequest.Page, pagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<ZodiacHouse> result = _work.ZodiacHouses.FindZodiacHouseWithAllData(filter, p => p.Id, out total);
                    return result;
                }
            } catch (Exception ex) { throw new ArgumentException("ZodiacHouseService : " + ex.Message); }
        }
        public ZodiacHouse UpdateZodiacHouse(int id, UpdateZodiacHouseRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            House checkHouse = _work.Houses.Get(request.HouseId);
            Zodiac checkZodiac = _work.Zodiacs.Get(request.ZodiacId);
            if (checkHouse == null)
            {
                throw new ArgumentException("House not exist ");
            }
            if (checkZodiac == null)
            {
                throw new ArgumentException("Zodiac not exist");
            }
            ZodiacHouse zodiacHouse = _work.ZodiacHouses.Get(id);
            if(zodiacHouse != null)
            {
                if (request.ZodiacId > 0)
                {
                    zodiacHouse.ZodiacId = request.ZodiacId;
                }
                if (zodiacHouse.HouseId > 0)
                {
                    zodiacHouse.HouseId = request.HouseId;
                }
                if (!string.IsNullOrWhiteSpace(request.Content))
                {
                    zodiacHouse.Content = request.Content;
                }
                return zodiacHouse;
            }
            else
            {
                throw new ArgumentException("This ZodiacHouse not found");
            }  
        }
        public ZodiacHouse DeleteZodiacHouse(int id)
        {           
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            ZodiacHouse zodiacHouse = _work.ZodiacHouses.Get(id);
            if (zodiacHouse != null)
            {
                _work.ZodiacHouses.Remove(zodiacHouse);
                return zodiacHouse;
            }
            else { throw new ArgumentException("This ZodiacHouse not found"); }
        }
        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
