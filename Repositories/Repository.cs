using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AstroBackEnd.Repositories
{
    public class Repository<TModel> : IRepository<TModel> where TModel : class
    {

        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            this._context = context;
        }


        public TModel Get(int id)
        {
            return _context.Set<TModel>().Find(id);
        }
        public IEnumerable<TModel> GetAll<TOrderBy>(Func<TModel, TOrderBy> orderBy)
        {
            return _context.Set<TModel>().OrderBy(orderBy).ToList();
        }

        public IEnumerable<TModel> GetAllPaging<TOrderBy>(Func<TModel, TOrderBy> orderBy, int page = 1, int pageSize = 20)
        {
            return _context.Set<TModel>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize + 1); // pageSize + 1 for hasNext - if have more than page size then hasNext = true
        }
        public IEnumerable<TModel> Find<TOrderBy>(Func<TModel, bool> predicate, Func<TModel, TOrderBy> orderBy)
        {
            return _context.Set<TModel>()
                .Where(predicate)
                .OrderBy(orderBy);
        }
        public IEnumerable<TModel> FindPaging<TOrderBy>(Func<TModel, bool> predicate, Func<TModel, TOrderBy> orderby , int page = 1, int pageSize = 20 )
        {
            return _context.Set<TModel>()
                .Where(predicate)
                .OrderBy(orderby)
                .Skip((page - 1) * pageSize)
                .Take(pageSize + 1); // pageSize + 1 for hasNext - if have more than page size then hasNext = true
        }

        
        public TModel Add(TModel model)
        {
            _context.Set<TModel>().Add(model);
            _context.SaveChanges();
            _context.Entry(model).GetDatabaseValues();
            return model;
        }

        public void AddAll(IEnumerable<TModel> models)
        {
            _context.Set<TModel>().AddRange(models);
        }

        public void Remove(TModel model)
        {
            _context.Set<TModel>().Remove(model);
        }

        public void RemoveAll(IEnumerable<TModel> models)
        {
            _context.Set<TModel>().RemoveRange(models);
        }

       

        
    }
}
