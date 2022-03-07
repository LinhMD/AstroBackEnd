using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetHouseRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Services.Implement
{
    public class PlanetHouseService : IPlanetHouseService, IDisposable
    {
        private readonly IUnitOfWork _work;
        public PlanetHouseService(IUnitOfWork _work)
        {
            this._work = _work;
        }
        public PlanetHouse GetPlanetHouse(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            PlanetHouse planetHouse = _work.PlanetHouses.Get(id);
            if (planetHouse != null)
            {
                return planetHouse;
            }
            else { throw new ArgumentException("This PlanetHouse not found"); }
            
        }

        public PlanetHouse CreatePlanetHouse(CreatePlanetHouseRequest request)
        {
            try
            {
                PlanetHouse planetHouse = new PlanetHouse()
                {
                    PlanetId = request.PlanetId,
                    HouseId = request.HouseId,
                    Content = request.Content,
                };
                return _work.PlanetHouses.Add(planetHouse);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("PlanetHouseService : " + ex.Message);
            }
        }

        public IEnumerable<PlanetHouse> FindPlanetHouse(FindPlanetHouseRequest request, out int total)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<PlanetHouse, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkHouseId = true;
                    bool checkPlanetId = true;
                    bool checkContent = true;
                    if (request.Id > 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (request.HouseId > 0)
                    {
                        checkHouseId = p.HouseId == request.HouseId;
                    }
                    if (request.PlanetId > 0)
                    {
                        checkPlanetId = p.PlanetId == request.PlanetId;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Content))
                    {
                        checkContent = !string.IsNullOrWhiteSpace(p.Content) ? p.Content.Contains(request.Content) : false;
                    }
                    return checkId && checkContent && checkPlanetId && checkHouseId;
                };
                PagingRequest pagingRequest = request.PagingRequest;
                Validation.ValidNumberThanZero(pagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(pagingRequest.PageSize, "PageSize must be than zero");
                if (pagingRequest != null)
                {
                    switch (pagingRequest.SortBy)
                    {
                        case "PlanetId":
                            return _work.PlanetHouses.FindPaging(filter, p => p.PlanetId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "HouseId":
                            return _work.PlanetHouses.FindPaging(filter, p => p.HouseId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        default:
                            return _work.PlanetHouses.FindPaging(filter, p => p.Id, out total, pagingRequest.Page, pagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<PlanetHouse> result = _work.PlanetHouses.Find(filter, p => p.Id);
                    total = result.Count();
                    return result;
                }
            }
            catch (Exception ex) { throw new ArgumentException("PlanetHouseService : " + ex.Message); }
        }

        public PlanetHouse UpdatePlanetHouse(int id, UpdatePlanetHouseRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            PlanetHouse planetHouse = _work.PlanetHouses.Get(id);
            if (planetHouse != null)
            {
                if (request.PlanetId > 0)
                {
                    planetHouse.PlanetId = request.PlanetId;
                }
                if (request.HouseId > 0)
                {
                    planetHouse.HouseId = request.HouseId;
                }
                return planetHouse;
            }
            else
            {
                throw new ArgumentException("This PlanetHouse not found");
            }
        }
        public PlanetHouse DeletePlanetHouse(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            PlanetHouse planetHouse = _work.PlanetHouses.Get(id);
            if (planetHouse != null)
            {
                _work.PlanetHouses.Remove(planetHouse);
                return planetHouse;
            }
            else { throw new ArgumentException("This PlanetHouse not found"); }

        }

        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
