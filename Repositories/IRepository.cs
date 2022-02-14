using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories
{
    public interface IRepository<TModel> where TModel : class
    {
        TModel Get(int id);

        IEnumerable<TModel> GetAll();

        IEnumerable<TModel> GetAllPaging(int page = 1, int pageSize = 20);

        IEnumerable<TModel> Find(Expression<Func<TModel, bool>> predicate);

        IEnumerable<TModel> FindPaging(Expression<Func<TModel, bool>> predicate, int page = 1, int pageSize = 20);

        void Add(TModel model);
        void AddAll(IEnumerable<TModel> models);

        void Remove(TModel model);
        void RemoveAll(IEnumerable<TModel> models);


    }
}
