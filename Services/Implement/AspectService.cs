using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.AspectRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Services.Implement
{
    public class AspectService : IAspectService, IDisposable
    {

        private readonly IUnitOfWork _work;
        public AspectService(IUnitOfWork _work)
        {
            this._work = _work;
        }
        public Aspect CreateAspect(CreateAspectRequest request)
        {
            try
            {
                Planet checkPlanetBase = _work.Planets.Get(request.PlanetBaseId);
                Planet checkPlanetCompare = _work.Planets.Get(request.PlanetCompareId);
                if (checkPlanetBase == null)
                {
                    throw new ArgumentException("Planet Base not exist ");
                }
                if (checkPlanetCompare == null)
                {
                    throw new ArgumentException("Planet Compare not exist");
                }
                Aspect aspect = new Aspect()
                {
                    PlanetBaseId = request.PlanetBaseId,
                    PlanetCompareId = request.PlanetCompareId,
                    AngleType = request.AngleType,
                    Description = request.Description,
                    MainContent = request.MainContent,
                };
                return _work.Aspects.Add(aspect);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("AspectService : CreateAspect : " + ex.Message);
            }
        }
        public Aspect GetAspect(int id)
        {
            try
            {
                Validation.ValidNumberThanZero(id, "Id must be than zero");
                Aspect checkAspect = _work.Aspects.Get(id);
                if (checkAspect != null)
                {
                    return _work.Aspects.GetAspectWithAllData(id);
                }
                else { throw new ArgumentException("This Aspect not found"); }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("AspectService : GetAspect : " + ex.Message);
            }
        }

        public IEnumerable<Aspect> FindAspect(FindAspectRequest request, out int total)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<Aspect, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkPlanetBaseId = true;
                    bool checkPlanetCompareId = true;
                    bool checkAngleType = true;
                    if (request.Id > 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (request.PlanetBaseId > 0)
                    {
                        checkPlanetBaseId = p.PlanetBaseId == request.PlanetBaseId;
                    }
                    if (request.PlanetCompareId > 0)
                    {
                        checkPlanetCompareId = p.PlanetCompareId == request.PlanetCompareId;
                    }
                    if (request.AngleType > 0)
                    {
                        checkAngleType = p.AngleType == request.AngleType;
                    }
                    return checkId && checkPlanetBaseId && checkPlanetCompareId && checkAngleType;
                };
                PagingRequest pagingRequest = request.PagingRequest;
                Validation.ValidNumberThanZero(pagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(pagingRequest.PageSize, "PageSize must be than zero");
                if (pagingRequest != null)
                {
                    switch (pagingRequest.SortBy)
                    {
                        case "PlanetBaseId":
                            return _work.Aspects.FindAspectWithAllData(filter, p => p.PlanetBaseId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "PlanetCompareId":
                            return _work.Aspects.FindAspectWithAllData(filter, p => p.PlanetCompareId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "AngleType":
                            return _work.Aspects.FindAspectWithAllData(filter, p => p.AngleType, out total, pagingRequest.Page, pagingRequest.PageSize);
                        default:
                            return _work.Aspects.FindAspectWithAllData(filter, p => p.Id, out total, pagingRequest.Page, pagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<Aspect> result = _work.Aspects.FindAspectWithAllData(filter, p => p.Id, out total);
                    total = result.Count();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("AspectService : FindAspect : " + ex.Message);
            }
        }

        public Aspect UpdateAspect(int id, UpdateAspectRequest request)
        {
            try
            {
                Validation.ValidNumberThanZero(id, "Id must be than zero");
                Planet checkPlanetBase = _work.Planets.Get(request.PlanetBaseId);
                Planet checkPlanetCompare = _work.Planets.Get(request.PlanetCompareId);
                if (checkPlanetBase == null)
                {
                    throw new ArgumentException("Planet Base not exist ");
                }
                if (checkPlanetCompare == null)
                {
                    throw new ArgumentException("Planet Compare not exist");
                }
                Aspect aspect = _work.Aspects.Get(id);
                if (aspect != null)
                {
                    if (request.PlanetBaseId > 0)
                    {
                        aspect.PlanetBaseId = request.PlanetBaseId;
                    }
                    if (request.PlanetCompareId > 0)
                    {
                        aspect.PlanetCompareId = request.PlanetCompareId;
                    }
                    if (request.AngleType > 0)
                    {
                        aspect.AngleType = request.AngleType;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Description))
                    {
                        aspect.Description = request.Description;
                    }
                    if (!string.IsNullOrWhiteSpace(request.MainContent))
                    {
                        aspect.MainContent = request.MainContent;
                    }
                    return aspect;
                }
                else
                {
                    throw new ArgumentException("This Aspect not found");
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("AspectService : UpdateAspect : " + ex.Message);
            }
        }

        public Aspect DeleteAspect(int id)
        {
            try
            {
                Validation.ValidNumberThanZero(id, "Id must be than zero");
                Aspect aspect = _work.Aspects.Get(id);
                if (aspect != null)
                {
                    _work.Aspects.Remove(aspect);
                    return aspect;
                }
                else { throw new ArgumentException("This Aspect not found"); }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("AspectService : DeleteAspect : " + ex.Message);
            }
        }

        public void Dispose()
        {
            _work.Complete();
        }
    }
}