using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.ZodiacHouseRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;

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
            ZodiacHouse zodiacHouse = _work.ZodiacHouses.Get(id);
            if (zodiacHouse != null)
            {
                return zodiacHouse;
            }
            else { throw new ArgumentException("This ZodiacHouse not found"); }
        }
        public ZodiacHouse CreateZodiacHouse(CreateZodiacHouseRequest request)
        {
            try
            {
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

        public IEnumerable<ZodiacHouse>  FindZodiacHouse(FindZodiacHouse request)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<ZodiacHouse, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkZodiacId = true;
                    bool checkHouseId = true;
                    bool checkContent = true;

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
                    if (!string.IsNullOrWhiteSpace(request.Content))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Content))
                        {
                            checkContent = p.Content.Contains(request.Content);
                        }
                        else
                        {
                            checkContent = false;
                        }
                    }
                    return checkId && checkHouseId && checkZodiacId && checkContent;
                };
                PagingRequest pagingRequest = request.PagingRequest;
                Validation.ValidNumberThanZero(pagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(pagingRequest.PageSize, "PageSize must be than zero");
                if (pagingRequest == null || pagingRequest.SortBy == null)
                {
                    return _work.ZodiacHouses.Find(filter, p => p.Id);
                }
                else
                {
                    switch (pagingRequest.SortBy)
                    {
                        case "ZodiacId":
                            return _work.ZodiacHouses.FindPaging(filter, p => p.ZodiacId, pagingRequest.Page, pagingRequest.PageSize);
                        case "HouseId":
                            return _work.ZodiacHouses.FindPaging(filter, p => p.HouseId, pagingRequest.Page, pagingRequest.PageSize);
                        default:
                            return _work.ZodiacHouses.FindPaging(filter, p => p.Id, pagingRequest.Page, pagingRequest.PageSize);
                    }
                }
            } catch (Exception ex) { throw new ArgumentException("ZodiacHouseService : " + ex.Message); }
        }
        public ZodiacHouse UpdateZodiacHouse(int id, UpdateZodiacHouseRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
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
