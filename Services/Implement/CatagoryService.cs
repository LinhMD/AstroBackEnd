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
            return _work.Catagorys.Add(catagorys);
        }

        public Catagory DeleteCatagory(int id)
        {            
            Catagory catagory = _work.Catagorys.Get(id);
            if(catagory != null)
            {
                _work.Catagorys.Remove(GetCatagory(id));
                return catagory;
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

        public IEnumerable<Catagory> FindCatagory(FindCatagoryRequest request)
        {
            Func<Catagory, bool> filter = p =>
            {
                bool checkId = request.Id == null ? true : p.Id == request.Id;

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
                    case "Id":
                        result = _work.Catagorys.FindPaging(filter, p => p.Id, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    case "Name":
                        result = _work.Catagorys.FindPaging(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;
                    default:
                        result = _work.Catagorys.FindPaging(filter, p => p.Id, request.PagingRequest.Page, request.PagingRequest.PageSize);
                        break;

                }
            }
            else
            {
                result = _work.Catagorys.Find(filter, p => p.Name);
            }

            return result;
        }

        public IEnumerable<Catagory> GetAllCatagory()
        {
            return _work.Catagorys.GetAll<String>(p => p.Name);
        }

        public Catagory GetCatagory(int id)
        {
            return _work.Catagorys.Get(id);
        }

        public Catagory UpdateCatagory(int id, CatagoryUpdateRequest request)
        {
            var catagory = _work.Catagorys.Get(id);
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                catagory.Name = request.Name;
            }
            return catagory;
        }
    }
}
