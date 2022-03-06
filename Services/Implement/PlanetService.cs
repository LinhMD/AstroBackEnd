using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.Utilities;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Implement
{
    public class PlanetService : IPlanetService, IDisposable
    {
        private readonly IUnitOfWork _work;
        public PlanetService(IUnitOfWork _work)
        {
            this._work = _work;
        }

        public Planet GetPlanet(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Planet planet = _work.Planets.Get(id);
            if (planet != null)
            {
                return planet;
            }
            else { throw new ArgumentException("This Planet not found"); }
        }

        public IEnumerable<Planet> FindPlanet(FindPlanetRequest request)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<Planet, bool> filter = p =>
                {
                    bool checkId = true;
                    bool checkName = true;
                    bool checkTitle = true;
                    bool chceckIcon = true;
                    bool checkDescription = true;
                    bool checkTag = true;
                    bool checkMainContent = true;

                    if (request.Id > 0)
                    {
                        checkId = p.Id == request.Id;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Name))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Name))
                        {
                            checkName = p.Name.Contains(request.Name);
                        }
                        else
                        {
                            checkName = false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(request.Title))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Title))
                        {
                            checkTitle = p.Title.Contains(request.Title);
                        }
                        else
                        {
                            checkTitle = false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(request.Icon))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Icon))
                        {
                            chceckIcon = p.Icon.Contains(request.Icon);
                        }
                        else
                        {
                            chceckIcon = false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(request.Description))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Decription))
                        {
                            checkDescription = p.Decription.Contains(request.Description);
                        }
                        else
                        {
                            checkDescription = false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(request.MainContent))
                    {
                        if (!string.IsNullOrWhiteSpace(p.MainContent))
                        {
                            checkMainContent = p.MainContent.Contains(request.MainContent);
                        }
                        else
                        {
                            checkMainContent = false;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(request.Tag))
                    {
                        if (!string.IsNullOrWhiteSpace(p.Tag))
                        {
                            checkTag = p.Tag.Contains(request.Tag);
                        }
                        else
                        {
                            checkTag = false;
                        }

                    }
                    return checkId && checkName && checkDescription && checkMainContent && checkTag && checkTitle;
                };
                PagingRequest paging = request.PagingRequest;
                Validation.ValidNumberThanZero(paging.Page, "Page must be than zero");
                Validation.ValidNumberThanZero(paging.PageSize, "PageSize must be than zero");
                if (paging == null || paging.SortBy == null)
                {
                    return _work.Planets.Find(filter, p => p.Id);
                }
                else
                {
                    switch (paging.SortBy)
                    {
                        case "Name":
                            return _work.Planets.FindPaging(filter, p => p.Name, paging.Page, paging.PageSize);
                        default:
                            return _work.Planets.FindPaging(filter, p => p.Id, paging.Page, paging.PageSize);
                    }
                }
            }
            catch (Exception ex) { throw new ArgumentException("PlanetService : " + ex.Message); }
        }

        public Planet CreatePlanet(CreatePlanetRequest request)
        {
            try
            {
                Planet planet = new Planet()
                {
                    Name = request.Name,
                    Decription = request.Description,
                    Icon = request.Icon,
                    MainContent = request.MainContent,
                    Tag = request.Tag,
                    Title = request.Title,
                };
                return _work.Planets.Add(planet);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("PlanetService : " + ex.Message);
            }          
        }

        public Planet DeletePlanet(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Planet planet = _work.Planets.Get(id);
            if (planet != null)
            {
                _work.Planets.Remove(planet);
                return planet;
            }
            else { throw new ArgumentException("This Planet not found"); }
        }

        public Planet UpdatePlanet(int id, UpdatePlanetRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Planet planet = _work.Planets.Get(id);
            if (planet != null)
            {
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    planet.Name = request.Name;
                }
                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    planet.Title = request.Title;
                }
                if (!string.IsNullOrWhiteSpace(request.Icon))
                {
                    planet.Icon = request.Icon;
                }
                if (!string.IsNullOrWhiteSpace(request.Description))
                {
                    planet.Decription = request.Description;
                }
                if (!string.IsNullOrWhiteSpace(request.Tag))
                {
                    planet.Tag = request.Tag;
                }
                if (!string.IsNullOrWhiteSpace(request.MainContent))
                {
                    planet.MainContent = request.MainContent;
                }
                return planet;
            }
            else
            {
                throw new ArgumentException("This Planet not found");
            }
        }

        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
