using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

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

        public IEnumerable<TModel> GetAll()
        {
            return _context.Set<TModel>().ToList();
        }

        public IEnumerable<TModel> Find(Expression<Func<TModel, bool>> predicate)
        {
            return _context.Set<TModel>().Where(predicate);
        }

        public IEnumerable<TModel> FindPaging(Expression<Func<TModel, bool>> predicate, int page = 1, int pageSize = 20)
        {
            return _context.Set<TModel>()
                .Where(predicate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }

        public IEnumerable<TModel> GetAllPaging(int page = 1, int pageSize = 20)
        {
            return _context.Set<TModel>()
                .Skip((page - 1) * pageSize)
                .Take(pageSize);
        }
        public void Add(TModel model)
        {
            _context.Set<TModel>().Add(model);
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
