using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstroBackEnd.RequestModels.CatagoryRequest;

namespace AstroBackEnd.Services.Core
{
    public interface ICatagorysService
    {
        public Catagory GetCatagory(int id);

        public IEnumerable<Catagory> GetAllCatagory();

        public IEnumerable<Catagory> FindCatagory(FindCatagoryRequest request);

        public void DeleteCatagory(int id);

        public Catagory UpdateCatagory(int id, CatagoryUpdateRequest request);

        public Catagory CreateCatagory(CatagoryCreateRequest request);
    }
}
