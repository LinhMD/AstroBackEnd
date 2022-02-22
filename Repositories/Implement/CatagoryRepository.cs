﻿using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories.Implement
{

    public class CatagoryRepository : Repository<Catagory>, ICatagoryRepository
    {
        public CatagoryRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }

        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public Catagory GetAllCatagoryData(int id)
        {
            return AstroData.Catagories.First(u => u.Id == id);
        }
    }
}