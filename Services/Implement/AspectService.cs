using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.AspectRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using AstroBackEnd.ViewsModel;
using SwissEphNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AstroBackEnd.Services.Implement
{
    public class AspectService : IAspectService, IDisposable
    {

        public static readonly (int, double, double) Conjunction = (0, 0, 8);

        public static readonly (int, double, double) SemiSextile = (1, 28, 32);

        public static readonly (int, double, double) SemiSquare = (2, 43, 55);

        public static readonly (int, double, double) Sextile = (3, 56, 64);

        public static readonly (int, double, double) Quintile = (4, 70, 72);

        public static readonly (int, double, double) Square = (5, 82, 98);

        public static readonly (int, double, double) Trine = (6, 112, 128);

        public static readonly (int, double, double) Sesquiquadrate = (7, 133, 137);
        
        public static readonly (int, double, double) BiQuintile = (8, 142, 146);

        public static readonly (int, double, double) Quincunx = (9, 148, 152);

        public static readonly (int, double, double) Opposition = (10, 172, 188);

        public static readonly Dictionary<string, (int angleType, double lower, double upper)> Aspects = new Dictionary<string, (int, double, double)>()
        {
            {"Conjunction", Conjunction},
            {"SemiSextile", SemiSextile},
            {"SemiSquare", SemiSquare},
            {"Sextile", Sextile},
            {"Quintile", Quintile},
            {"Square", Square},
            {"Trine", Trine},
            {"Sesquiquadrate", Sesquiquadrate},
            {"BiQuintile", BiQuintile},
            {"Quincunx", Quincunx},
            {"Opposition", Opposition},
        };



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
                    throw new ArgumentException("Planet Base found ");
                }
                if (checkPlanetCompare == null)
                {
                    throw new ArgumentException("Planet Compare not found");
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
                /*Func<Aspect, bool> filter = p =>
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
                    if (request.AngleType >= 0)
                    {
                        checkAngleType = p.AngleType == request.AngleType;
                    }
                    return checkId && checkPlanetBaseId && checkPlanetCompareId && checkAngleType;
                };*/

                Expression<Func<Aspect, bool>> filter = a => (request.Id <= 0 || a.Id == request.Id) &&
                                                            (request.PlanetBaseId <= 0 || a.PlanetBaseId == request.PlanetBaseId) &&
                                                            (request.PlanetCompareId <= 0 || a.PlanetCompareId == request.PlanetCompareId) &&
                                                            (request.AngleType < 0 || a.AngleType == request.AngleType);
                PagingRequest pagingRequest = request.PagingRequest;
                Validation.ValidNumberThanZero(pagingRequest.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(pagingRequest.PageSize, "PageSize must be than zero");
                if (pagingRequest != null)
                {
                    switch (pagingRequest.SortBy)
                    {
                        case "PlanetBaseId":
                            return _work.Aspects.FindAtDBPaging(filter, p => p.PlanetBaseId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "PlanetCompareId":
                            return _work.Aspects.FindAtDBPaging(filter, p => p.PlanetCompareId, out total, pagingRequest.Page, pagingRequest.PageSize);
                        case "AngleType":
                            return _work.Aspects.FindAtDBPaging(filter, p => p.AngleType, out total, pagingRequest.Page, pagingRequest.PageSize);
                        default:
                            return _work.Aspects.FindAtDBPaging(filter, p => p.Id, out total, pagingRequest.Page, pagingRequest.PageSize);
                    }
                }
                else
                {
                    IEnumerable<Aspect> result = _work.Aspects.FindAtDBPaging(filter, p => p.Id, out total);
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
                    throw new ArgumentException("Planet Base not found ");
                }
                if (checkPlanetCompare == null)
                {
                    throw new ArgumentException("Planet Compare not found");
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

        public Dictionary<string, List<HoroscopeItemView>> CalculateAspect(DateTime birthDate, DateTime compareDate)
        {
            Dictionary<string, (int id , Planet planet)> planetDic = new Dictionary<string, (int, Planet)>();

            IEnumerable<Planet> planets = _work.Planets.GetAll(p => p.Id);
            Console.WriteLine(DateTimeOffset.Now.ToUnixTimeMilliseconds());
            foreach (var planet in planets)
            {
                planetDic[planet.Tag.Trim().ToLower()] = (planet.Id, planet);
            }
            
            using SwissEph swiss = new();
            swiss.swe_set_ephe_path(null);

            string error = "";
            double[] birthPos = new double[6];
            double[] comparePos = new double[6];
            double birthDayJuli = swiss.swe_julday(birthDate.Year, birthDate.Month, birthDate.Day, birthDate.Hour, SwissEph.SE_GREG_CAL);
            double compareJuli = swiss.swe_julday(compareDate.Year, compareDate.Month, compareDate.Day, compareDate.Hour, SwissEph.SE_GREG_CAL);
            HashSet<int> aspectIds = new HashSet<int>();

            for (int planet = SwissEph.SE_SUN; planet <= SwissEph.SE_PLUTO; planet++)
            {
                swiss.swe_calc_ut(birthDayJuli, planet, SwissEph.SEFLG_SPEED, birthPos, ref error);

                string planetNameBirth = swiss.swe_get_planet_name(planet).ToLower().Trim();
                double birthLongtitude = birthPos[0];
                
                for (int planetCompare = SwissEph.SE_SUN; planetCompare <= SwissEph.SE_PLUTO; planetCompare++)
                {
                    swiss.swe_calc_ut(compareJuli, planetCompare, SwissEph.SEFLG_SPEED, comparePos, ref error);

                    string planetNameCompare = swiss.swe_get_planet_name(planetCompare).ToLower().Trim();

                    double compareLongtitude = comparePos[0];

                    double angle = Math.Abs(compareLongtitude - birthLongtitude);

                    foreach (var aspectName in Aspects.Keys) 
                    {
                        (int angleType, double lower, double upper) = Aspects[aspectName];

                        if(angle >= lower && angle <= upper)
                        {
                            int idBase = planetDic[planetNameBirth].id;
                            int idCompare = planetDic[planetNameCompare].id;
                            int aspectId = _work.Aspects.Find(a => a.PlanetBaseId == idBase && a.PlanetCompareId == idCompare && a.AngleType == angleType, a => a.Id).Select(a => a.Id).FirstOrDefault();

                            if(aspectId > 0)
                                aspectIds.Add(aspectId);
                        }
                    }
                }
            }

            IEnumerable<HoroscopeItem> HoroItem = _work.HoroscopeItems.FindHoroscopeItemWithAllData(h => aspectIds.Contains(h.AspectId), h => h.AspectId, out int total);

            var lifeAttributeHoro = new Dictionary<string, List<HoroscopeItemView>>();

            IEnumerable<LifeAttribute> lifeAttrs = _work.LifeAttributes.GetAll(l => l.Id);

            foreach (var lifeAttr in lifeAttrs)
            {
                lifeAttributeHoro[lifeAttr.Name] = new List<HoroscopeItemView>();
            }

            foreach (var item in HoroItem)
            {
                lifeAttributeHoro[item.LifeAttribute.Name].Add(new HoroscopeItemView(item));
            }

            return lifeAttributeHoro;
        }
    }
}