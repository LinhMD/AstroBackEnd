using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetZodiacRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

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
            PlanetZodiac checkPlanetZodiac = _work.PlanetZodiacs.Get(id);
            if (checkPlanetZodiac != null)
            {
                var planetZodiac = _work.PlanetZodiacs.GetPlanetZodiacWithAllData(id);
                return planetZodiac;
            }
            else { throw new ArgumentException("This PlanetZodiac not found"); }
        }

        public PlanetZodiac CreatePlanetZodiac(CreatePlanetZodiacRequest request)
        {
            try
            {
                Planet checkPlanet = _work.Planets.Get(request.PlanetId);
                Zodiac checkZodiac = _work.Zodiacs.Get(request.ZodiacId);
                if (checkPlanet == null)
                {
                    throw new ArgumentException("Planet not exist ");
                }
                if (checkZodiac == null)
                {
                    throw new ArgumentException("Zodiac not exist");
                }
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

        public IEnumerable<PlanetZodiac> FindPlanetZodiac(FindPlanetZodiacRequest request, out int total)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<PlanetZodiac, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkZodiacId = true;
                    bool checkPlanetId = true;
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
                    return checkId && checkPlanetId && checkZodiacId;
                };
                PagingRequest pagingRequest = request.PagingRequest;
                Validation.ValidNumberThanZero(pagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(pagingRequest.PageSize, "PageSize must be than zero");
                if (pagingRequest != null)
                {
                    switch (pagingRequest.SortBy)
                    {
                        case "PlanetId":
                            return _work.PlanetZodiacs.FindPPaginglanetZodiacWithAllData(filter, p => p.PlanetId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "ZodiacId":
                            return _work.PlanetZodiacs.FindPPaginglanetZodiacWithAllData(filter, p => p.ZodiacId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        default:
                            return _work.PlanetZodiacs.FindPPaginglanetZodiacWithAllData(filter, p => p.Id, out total, pagingRequest.Page, pagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<PlanetZodiac> result = _work.PlanetZodiacs.FindPlanetZodiacWithAllData(filter, p => p.Id, out total);
                    return result;
                }
            } catch (Exception ex) { throw new ArgumentException("PlanetZodiacService : " + ex.Message); }
        }

        public PlanetZodiac UpdatePlanetZodiac(int id, UpdatePlanetZodiacRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Planet checkPlanet = _work.Planets.Get(request.PlanetId);
            Zodiac checkZodiac = _work.Zodiacs.Get(request.ZodiacId);
            if (checkPlanet == null)
            {
                throw new ArgumentException("Planet not exist ");
            }
            if (checkZodiac == null)
            {
                throw new ArgumentException("Zodiac not exist");
            }
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
