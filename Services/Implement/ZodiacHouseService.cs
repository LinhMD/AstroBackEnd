using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.ZodiacHouseRequest;
using AstroBackEnd.Services.Core;
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
            return _work.ZodiacHouses.Get(id);
        }
        public ZodiacHouse CreateZodiacHouse(CreateZodiacHouseRequest request)
        {
            Zodiac zodiac = _work.Zodiacs.Get(request.ZodiacId);
            House house = _work.Houses.Get(request.HouseId);
            ZodiacHouse zodiacHouse = new ZodiacHouse()
            {
                HouseId = request.HouseId,
                ZodiacId = request.ZodiacId,
                Zodiac = zodiac,
                House = house,
                Content = request.Content
            };
            _work.ZodiacHouses.Add(zodiacHouse);
            return zodiacHouse;
        }

        public IEnumerable<ZodiacHouse>  FindZodiacHouse(FindZodiacHouse request)
        {
            Func<ZodiacHouse, bool> filter = p =>
            {
                bool checkId = true;
                bool checkZodiacId = true;
                bool checkHouseId = true;
                bool checkContent = true;
                
                if(request.Id > 0 )
                {
                    checkId = request.Id == p.Id;
                }
                if(request.HouseId > 0)
                {
                    checkHouseId = request.HouseId == p.HouseId;  
                }
                if(request.ZodiacId > 0)
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
                        checkContent   = false;
                    }
                }
                return checkId && checkHouseId && checkZodiacId && checkContent;
            };
            PagingRequest pagingRequest = request.PagingRequest;

            if(pagingRequest == null || pagingRequest.SortBy == null)
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
        }
        public ZodiacHouse UpdateZodiacHouse(int id, UpdateZodiacHouseRequest request)
        {
            ZodiacHouse zodiacHouse = _work.ZodiacHouses.Get(id);
            if(zodiacHouse != null)
            {
                if (request.ZodiacId > 0)
                {
                    Zodiac zodiac = _work.Zodiacs.Get(request.ZodiacId);
                    zodiacHouse.Zodiac = zodiac != null ? zodiac : null;
                    zodiacHouse.ZodiacId = request.ZodiacId;
                }
                if (zodiacHouse.HouseId > 0)
                {
                    House house = _work.Houses.Get(request.HouseId);
                    zodiacHouse.House = house != null ? house : null;
                    zodiacHouse.ZodiacId = request.ZodiacId;
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
                return null;
            }  
        }
        public ZodiacHouse DeleteZodiacHouse(int id)
        {
            ZodiacHouse zodiacHouse = _work.ZodiacHouses.Get(id);
            if(zodiacHouse != null)
            {
                _work.ZodiacHouses.Remove(zodiacHouse);
                return zodiacHouse;
            }
            else
            {
                return null;
            }
        }
        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
