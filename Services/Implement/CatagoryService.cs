using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.Services.Core;
using AstroBackEnd.RequestModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstroBackEnd.RequestModels.CatagoryRequest;

namespace AstroBackEnd.Services.Implement
{
    public class CatagoryService : ICatagorysService, IDisposable
    {
        private readonly IUnitOfWork _work;

        private readonly AstroDataContext _astroData;
        public CatagoryService(IUnitOfWork work, AstroDataContext astroData)
        {
            this._work = work;
            this._astroData = astroData;
        }
        public Catagory CreateCatagory(CatagoryCreateRequest request)
        {
            Catagory catagorys = new Catagory()
            {
                Name = request.Name
            };
            return _work.Catagory.Add(catagorys);
        }

        public void DeleteCatagory(int id)
        {
            _work.Catagory.Remove(GetCatagory(id));
        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public IEnumerable<Catagory> FindCatagory(FindCatagoryRequest request)
        {
            Func<Catagory, bool> filter = p =>
            {
                bool checkName = true;
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    checkName = p.Name.Contains(request.Name);
                }

                
                return checkName ;
            };
            IEnumerable<Catagory> result = null;
            if (request.PagingRequest != null)
            {
                switch (request.PagingRequest.SortBy)
                {
                    case "Name":
                        result = _work.Catagory.FindPaging(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    
                }
            }
            else
            {
                result = _work.Catagory.Find(filter, p => p.Name);
            }

            return result;
        }

        public IEnumerable<Catagory> GetAllCatagory()
        {
            return _work.Catagory.GetAll<String>(p => p.Name);
        }

        public Catagory GetCatagory(int id)
        {
            return _work.Catagory.Get(id);
        }

        public Catagory UpdateCatagory(int id, CatagoryUpdateRequest request)
        {
            var catagory = _work.Catagory.Get(id);
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                catagory.Name = request.Name;
            }
            return catagory;
        }
    }
}
