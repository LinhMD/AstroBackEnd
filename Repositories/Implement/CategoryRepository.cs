using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories.Implement
{

    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }

        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

        public Category GetAllCategoryData(int id)
        {
            return AstroData.Categories.First(u => u.Id == id);
        }
    }
}
