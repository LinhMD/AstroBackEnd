using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.PlanetRequest;
using AstroBackEnd.Services.Core;
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
            return _work.Planets.Get(id);
        }

        public IEnumerable<Planet> FindPlanet(FindPlanetRequest request)
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

        public Planet CreatePlanet(CreatePlanetRequest request)
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

        public Planet DeletePlanet(int id)
        {
            Planet planet = _work.Planets.Get(id);
            if (planet == null)
                return null;
            else
            {
                _work.Planets.Remove(planet);
                return planet;
            }

        }

        public Planet UpdatePlanet(int id, UpdatePlanetRequest request)
        {
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
                return null;
            }
        }

        public void Dispose()
        {
            this._work.Complete();
        }
    }
}
