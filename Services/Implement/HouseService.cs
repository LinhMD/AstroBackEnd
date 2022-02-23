using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.HouseRequest;
using AstroBackEnd.Services.Core;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Services.Implement
{
    public class HouseService : IHouseService, IDisposable
    {
        private readonly IUnitOfWork _work;

        public HouseService(IUnitOfWork _work)
        {
            this._work = _work;
        }

        public House CreateHouse(CreateHouseRequest request)
        {
            House house = new House()
            {
                Name = request.Name,
                Title = request.Title,
                Decription = request.Description,
                Icon = request.Icon,
                Tag = request.Tag,
                MainContent = request.MainContentl,
            };
            return _work.Houses.Add(house);
        }

        public IEnumerable<House> FindHouse(FindHouseRequest request)
        {
            Func<House, bool> filter = p =>
            {
                 bool checkId = true;
                 bool checkNameHouse = true;
                 bool checkDescriptionHouse = true;
                 bool checkTitleHouse = true;
                 bool checkTag = true;
                 bool checkMainContent = true;
                 if (request.Id != null)
                 {
                    if(p.Id == request.Id)
                    {
                        checkId = true;
                    }
                    else
                    {
                        checkId = false;
                    }
                 }
                 if (!string.IsNullOrWhiteSpace(request.Name)){
                     if (!string.IsNullOrWhiteSpace(p.Name))
                     {
                         checkNameHouse = p.Name.Contains(request.Name);
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
                         checkTitleHouse = p.Title.Contains(request.Title);
                     }
                     else
                     {
                         checkTitleHouse= false;
                     }
                 }
                 if (!string.IsNullOrWhiteSpace(request.Description))
                 {
                     if (!string.IsNullOrWhiteSpace(p.Decription))
                     {
                         checkDescriptionHouse = p.Decription.Contains(request.Description);
                     }
                     else
                     {
                         checkDescriptionHouse = false;
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
                         checkMainContent= false;
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
                         checkTag= false;
                     }

                 }
                 return checkNameHouse && checkDescriptionHouse && checkMainContent && checkTag && checkTitleHouse;
             };
            PagingRequest paging = request.PagingRequest;
            if (paging == null || paging.SortBy == null)
            {
                return _work.Houses.Find(filter, p => p.Id);
            }
            else
            {
                switch (paging.SortBy)
                {
                    case "Name":
                        return _work.Houses.FindPaging(filter, p => p.Name, paging.Page, paging.PageSize);
                    default:
                        return _work.Houses.FindPaging(filter, p => p.Id, paging.Page, paging.PageSize);
                }
            }
        }

        public House DeleteHouse(int id)
        {
            House house = _work.Houses.Get(id);
            if (house == null)
            {
                return null;
            }
            _work.Houses.Remove(house);
            return house;
        }

        public House GetHouse(int id)
        {
            return _work.Houses.Get(id);
        }

        public House UpdateHouse(int id, UpdateHouseRequest request)
        {
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
                if (!string.IsNullOrWhiteSpace(request.MainContentl))
                {
                    house.MainContent = request.MainContentl;
                }
                return house;
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
