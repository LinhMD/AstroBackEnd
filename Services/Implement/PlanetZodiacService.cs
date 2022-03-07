using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetZodiacRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
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
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            PlanetZodiac planetZodiac = _work.PlanetZodiacs.Get(id);
            if (planetZodiac != null)
            {
                return planetZodiac;
            }
            else { throw new ArgumentException("This PlanetZodiac not found"); }
        }

        public PlanetZodiac CreatePlanetZodiac(CreatePlanetZodiacRequest request)
        {
            try
            {
                PlanetZodiac planetZodiac = new PlanetZodiac()
                {
                    PlanetId = request.PlanetId,
                    ZodiacId = request.ZodiacId,
                    Content = request.Content,
                };
                return _work.PlanetZodiacs.Add(planetZodiac);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("PlanetZodiacService : " + ex.Message);
            }      
        }

        public IEnumerable<PlanetZodiac> FindPlanetZodiac(FindPlanetZodiacRequest request)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<PlanetZodiac, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkZodiacId = true;
                    bool checkPlanetId = true;
                    bool checkContent = true;
                    if (request.Id > 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (request.ZodiacId > 0)
                    {
                        checkZodiacId = p.ZodiacId == request.ZodiacId;

                    }
                    if (request.PlanetId > 0)
                    {
                        checkPlanetId = p.PlanetId == request.PlanetId;
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
                Validation.ValidNumberThanZero(paging.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(paging.PageSize, "PageSize must be than zero");
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
            } catch (Exception ex) { throw new ArgumentException("PlanetZodiacService : " + ex.Message); }
        }

        public PlanetZodiac UpdatePlanetZodiac(int id, UpdatePlanetZodiacRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
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
            else
            {
                throw new ArgumentException("This PlanetZodiac not found");
            }
        }
        public PlanetZodiac DeletePlanetZodiac(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            PlanetZodiac planetZodiac = _work.PlanetZodiacs.Get(id);
            if (planetZodiac != null)
            {
                _work.PlanetZodiacs.Remove(planetZodiac);
                return planetZodiac;
            }
            else { throw new ArgumentException("This PlanetZodiac not found"); }
        }

        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
