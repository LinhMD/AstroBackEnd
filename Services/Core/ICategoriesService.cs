using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstroBackEnd.RequestModels.CategoryRequest;

namespace AstroBackEnd.Services.Core
{
    public interface ICategoriesService
    {
        public Category GetCategory(int id);

        public IEnumerable<Category> GetAllCategory();

        public IEnumerable<Category> FindCategory(FindCategoryRequest request);

        public IEnumerable<Category> FindCategory(FindCategoryRequest request, out int total);

        public Category DeleteCategory(int id);

        public Category UpdateCategory(int id, CategoryUpdateRequest request);

        public Category CreateCategory(CategoryCreateRequest request);
    }
}
