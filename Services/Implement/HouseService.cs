using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.HouseRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Services.Implement
{
    public class HouseService : IHouseService, IDisposable
    {
        private readonly IUnitOfWork _work;

        public HouseService(IUnitOfWork _work)
        {
            this._work = _work;
        }

        public House GetHouse(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            House house = _work.Houses.Get(id);
            if (house != null)
            {
                return house;
            }
            else { throw new ArgumentException("This House not found"); }
        }

        public House CreateHouse(CreateHouseRequest request)
        {
            try
            {
                House house = new House()
                {
                    Name = request.Name,
                    Title = request.Title,
                    Decription = request.Description,
                    Icon = request.Icon,
                    Tag = request.Tag,
                    MainContent = request.MainContent,
                };
                return _work.Houses.Add(house);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("HouseService : " + ex.Message);
            } 
        }

        public IEnumerable<House> FindHouse(FindHouseRequest request, out int total)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<House, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkNameHouse = true;
                    bool checkTitleHouse = true;
                    bool checkTag = true;
                    if (request.Id > 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Name))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Name))
                        {
                            checkNameHouse = p.Name.ToLower().Contains(request.Name.ToLower());
                        }
                        else
                        {
                            checkNameHouse = false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(request.Title))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Title))
                        {
                            checkTitleHouse = p.Title.ToLower().Contains(request.Title.ToLower());
                        }
                        else
                        {
                            checkTitleHouse = false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(request.Tag))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Tag))
                        {
                            checkTag = p.Tag.ToLower().Contains(request.Tag.ToLower());
                        }
                        else
                        {
                            checkTag = false;
                        }

                    }
                    return checkId && checkNameHouse && checkTag && checkTitleHouse;
                };
                PagingRequest pagingRequest = request.PagingRequest;
                Validation.ValidNumberThanZero(pagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(pagingRequest.PageSize, "PageSize must be than zero");

                if (pagingRequest != null)
                {
                    switch (pagingRequest.SortBy)
                    {
                        case "Name":
                            return _work.Houses.FindPaging(filter, p => p.Name, out total, pagingRequest.Page, pagingRequest.PageSize);
                        default:
                            return _work.Houses.FindPaging(filter, p => p.Id, out total, pagingRequest.Page, pagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<House> result = _work.Houses.Find(filter, p => p.Id);
                    total = result.Count();
                    return result;
                }
            }
            catch (Exception e) { throw new ArgumentException("HouseService : " + e.Message); }
        }

        public House DeleteHouse(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            House house = _work.Houses.Get(id);
            if (house != null)
            {
                _work.Houses.Remove(house);
                return house;
            }
            else { throw new ArgumentException("This House not found"); }
        }

        public House UpdateHouse(int id, UpdateHouseRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            House house = _work.Houses.Get(id);
            if(house != null)
            {
                if(!string.IsNullOrWhiteSpace(request.Name))
                {
                    house.Name = request.Name;
                }
                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    house.Title = request.Title;
                }
                if (!string.IsNullOrWhiteSpace(request.Icon))
                {
                    house.Icon = request.Icon;
                }
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    house.Decription = request.Description;
                }
                if (!string.IsNullOrWhiteSpace(request.Tag))
                {
                    house.Tag = request.Tag;
                }
                if (!string.IsNullOrWhiteSpace(request.MainContent))
                {
                    house.MainContent = request.MainContent;
                }
                return house;
            }
            else
            {
                throw new ArgumentException("This House not found");
            }
        }


        public void Dispose()
        {
            this._work.Complete();
        }

    }
}
