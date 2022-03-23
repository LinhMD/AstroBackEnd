using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AstroBackEnd.Repositories
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : class
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
                .Take(pageSize ); 
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
                .Take(pageSize); 
        }

        public IEnumerable<TModel> FindPaging<TOrderBy>(Func<TModel, bool> predicate, Func<TModel, TOrderBy> orderby, out int total, int page = 1, int pageSize = 20)
        {
            IOrderedEnumerable<TModel> models = _context.Set<TModel>()
                .Where(predicate)
                .OrderBy(orderby);

            total = models.Count();

            return models
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize); 
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

        public IQueryable<TModel> FindAtDBPaging<TOrderBy>(Expression<Func<TModel, bool>> predicate, Expression<Func<TModel, TOrderBy>> orderBy, out int total, int page = 1, int pageSize = 20)
        {
            IOrderedQueryable<TModel> models = this.WithAllData().Where(predicate).OrderBy(orderBy);

            total = models.Count();

            return models.Skip((page -1) * pageSize).Take(pageSize);
        }

        public abstract IQueryable<TModel> WithAllData();

    }
}
