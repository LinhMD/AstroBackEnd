using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetHouseRequest;
using AstroBackEnd.Services.Core;
using System;
using System.Collections.Generic;

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
            return _work.PlanetHouses.Get(id);
        }

        public PlanetHouse CreatePlanetHouse(CreatePlanetHouseRequest request)
        {
            House house = new House();
            Planet planet = new Planet();
            bool checkHouse = true;
            bool checkPlanet = true;
            if (request.HouseId > 0)
            {
                house = _work.Houses.Get(request.HouseId);
            }
            else
            {
                checkHouse = false;
                throw new Exception("HouseId must be than zero");
            }
            if (request.PlanetId > 0)
            {
                planet = _work.Planets.Get(request.PlanetId);
            }
            else
            {
                checkPlanet = false;
                throw new Exception("PlanetId must be than zero");
            }
            if (checkHouse && checkPlanet)
            {
                PlanetHouse planetHouse = new PlanetHouse()
                {
                    PlanetId = request.PlanetId,
                    Planet = planet,
                    HouseId = request.HouseId,
                    House = house,
                    Content = request.Content,
                };
                return _work.PlanetHouses.Add(planetHouse);
            }
            return null;
        }

        public IEnumerable<PlanetHouse> FindPlanetHouse(FindPlanetHouseRequest request)
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
            PagingRequest paging = request.PagingRequest;
            if (paging == null || paging.SortBy == null)
            {
                return _work.PlanetHouses.Find(filter, p => p.Id);
            }
            else
            {
                switch (paging.SortBy)
                {
                    case "PlanetId":
                        return _work.PlanetHouses.FindPaging(filter, p => p.PlanetId, paging.Page, paging.PageSize);
                    case "HouseId":
                        return _work.PlanetHouses.FindPaging(filter, p => p.HouseId, paging.Page, paging.PageSize);
                    default:
                        return _work.PlanetHouses.FindPaging(filter, p => p.Id, paging.Page, paging.PageSize);
                }
            }
        }

        public PlanetHouse UpdatePlanetHouse(int id, UpdatePlanetHouseRequest request)
        {
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
            return null;
        }
        public PlanetHouse DeletePlanetHouse(int id)
        {
            PlanetHouse planetHouse = _work.PlanetHouses.Get(id);
            if (planetHouse != null)
            {
                _work.PlanetHouses.Remove(planetHouse);
                return planetHouse;
            }
            return null;

        }

        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
