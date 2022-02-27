using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetZodiacRequest;
using AstroBackEnd.Services.Core;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Implement
{
    public class PlanetZodiacService : IPlanetZodiacService, IDisposable
    {
        private readonly IUnitOfWork _work;
        public PlanetZodiacService(IUnitOfWork _work)
        {
            this._work = _work;
        }
        public PlanetZodiac GetPlanetZodiac(int id)
        {
            return _work.PlanetZodiacs.Get(id);
        }

        public PlanetZodiac CreatePlanetZodiac(CreatePlanetZodiacRequest request)
        {
            Zodiac zodiac = new Zodiac();
            Planet planet = new Planet();
            bool checkZodiac = true;
            bool checkPlanet = true;
            if(request.ZodiacId > 0)
            {
                 zodiac = _work.Zodiacs.Get(request.ZodiacId);
 
            }
            else
            {
                checkZodiac = false;
                throw new Exception("ZodiacId must be than zero");
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
            if (checkZodiac && checkPlanet)
            {
                PlanetZodiac planetZodiac = new PlanetZodiac()
                {
                    PlanetId = request.PlanetId,
                    Planet = planet,
                    ZodiacId = request.ZodiacId,
                    Zodiac = zodiac,
                    Content = request.Content,
                };
                return _work.PlanetZodiacs.Add(planetZodiac);
            }
            return null;
            
        }

        public IEnumerable<PlanetZodiac> FindPlanetZodiac(FindPlanetZodiacRequest request)
        {
            Func<PlanetZodiac, bool> filter = p =>
            {
                bool checkId = true;
                bool checkZodiacId = true;
                bool checkPlanetId = true;
                bool checkContent = true;
                if (request.Id != 0)
                {
                    if (p.Id == request.Id)
                    {
                        checkId = true;
                    }
                    else
                    {
                        checkId = false;
                    }
                }
                if (request.ZodiacId != 0)
                {
                    if (p.ZodiacId == request.ZodiacId)
                    {
                        checkZodiacId = true;
                    }
                    else
                    {
                        checkZodiacId = false;
                    }
                }
                if (request.PlanetId != 0)
                {
                    if (p.PlanetId == request.PlanetId)
                    {
                        checkPlanetId = true;
                    }
                    else
                    {
                        checkPlanetId = false;
                    }
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
                return checkId && checkContent && checkPlanetId && checkZodiacId;
            };

            PagingRequest paging = request.PagingRequest;
            if (paging == null || paging.SortBy == null)
            {
                return _work.PlanetZodiacs.Find(filter, p => p.Id);
            }
            else
            {
                switch (paging.SortBy)
                {
                    case "PlanetId":
                        return _work.PlanetZodiacs.FindPaging(filter, p => p.PlanetId, paging.Page, paging.PageSize);
                    case "ZodiacId":
                        return _work.PlanetZodiacs.FindPaging(filter, p => p.ZodiacId, paging.Page, paging.PageSize);
                    default:
                        return _work.PlanetZodiacs.FindPaging(filter, p => p.Id, paging.Page, paging.PageSize);
                }
            }
        }

        public PlanetZodiac UpdatePlanetZodiac(int id, UpdatePlanetZodiacRequest request)
        {
            PlanetZodiac planetZodiac = _work.PlanetZodiacs.Get(id);
            if (planetZodiac != null)
            {
                if (request.PlanetId > 0)
                {
                    planetZodiac.PlanetId = request.PlanetId;
                }
                if (request.ZodiacId > 0)
                {
                    planetZodiac.ZodiacId = request.ZodiacId;
                }
                if (!string.IsNullOrWhiteSpace(request.Content))
                {
                    planetZodiac.Content = request.Content;
                }

                return planetZodiac;
            }
            return null;
        }
        public PlanetZodiac DeletePlanetZodiac(int id)
        {
            PlanetZodiac planetZodiac = _work.PlanetZodiacs.Get(id);
            if (planetZodiac == null)
                return null;
            else
            {
                _work.PlanetZodiacs.Remove(planetZodiac);
                return planetZodiac;
            }
        }

        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
