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
using AstroBackEnd.RequestModels.CategoryRequest;
using AstroBackEnd.Utilities;

namespace AstroBackEnd.Services.Implement
{
    public class CategoryService : ICategorysService, IDisposable
    {
        private readonly IUnitOfWork _work;

        private readonly AstroDataContext _astroData;
        public CategoryService(IUnitOfWork work, AstroDataContext astroData)
        {
            this._work = work;
            this._astroData = astroData;
        }
        public Category CreateCategory(CategoryCreateRequest request)
        {
            try
            {
                Category categories = new Category()
                {
                    Name = request.Name
                };
                return _work.Categorys.Add(categories);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("CategoryService : " + ex.Message);
            }
        }

        public Category DeleteCategory(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Category category = _work.Categorys.Get(id);
            if(category != null)
            {
                _work.Categorys.Remove(GetCategory(id));
                return category;
            }
            else
            {
                throw new ArgumentException("This category not found");
            }


        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public IEnumerable<Category> FindCategory(FindCategoryRequest request)
        {
            if (request.Id < 0) { throw new ArgumentException("Id must be equal or than zero"); }
            try
            {
                Func<Category, bool> filter = p =>
                {
                    bool checkId = request.Id == null ? true : p.Id == request.Id;

                    bool checkName = true;
                    if (!string.IsNullOrWhiteSpace(request.Name))
                    {
                        checkName = p.Name.Contains(request.Name);
                    }


                    return checkName;
                };
                IEnumerable<Category> result = null;
                if (request.PagingRequest != null)
                {
                    switch (request.PagingRequest.SortBy)
                    {
                        case "Id":
                            result = _work.Categorys.FindPaging(filter, p => p.Id, request.PagingRequest.Page, request.PagingRequest.PageSize);
                            break;
                        case "Name":
                            result = _work.Categorys.FindPaging(filter, p => p.Name, request.PagingRequest.Page, request.PagingRequest.PageSize);
                            break;
                        default:
                            result = _work.Categorys.FindPaging(filter, p => p.Id, request.PagingRequest.Page, request.PagingRequest.PageSize);
                            break;

                    }
                }
                else
                {
                    result = _work.Categorys.Find(filter, p => p.Name);
                }

                return result;
            }
            catch (Exception ex) { throw new ArgumentException("CategoryService : " + ex.Message); }
        }

        public IEnumerable<Category> GetAllCategory()
        {
            return _work.Categorys.GetAll<String>(p => p.Name);
        }

        public Category GetCategory(int id)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Category category = _work.Categorys.Get(id);
            if (category != null)
            {
                return category;
            }
            else { throw new ArgumentException("Can not find category with id(" + id + ")"); }
        }

        public Category UpdateCategory(int id, CategoryUpdateRequest request)
        {
            Validation.ValidNumberThanZero(id, "Id must be than zero");
            Category category = _work.Categorys.Get(id);
            if (category != null)
            {
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    category.Name = request.Name;
                }
                return category;
            }
            else
            {
                throw new ArgumentException("This category not found");
            }
        }
    }
}
