using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace AstroBackEnd.Repositories.Implement
{
    public class LifeAttributeRepository : Repository<LifeAttribute>, ILifeAttributeRepository
    {
        public LifeAttributeRepository(AstroDataContext dataContext) : base(dataContext)
        {
        }
        private AstroDataContext AstroDataContext { get { return base._context as AstroDataContext; } }
    }
}
