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

        IEnumerable<TModel> GetAll<TOrderBy>(Func<TModel, TOrderBy> orderBy);

        IEnumerable<TModel> GetAllPaging<TOrderBy>(Func<TModel, TOrderBy> orderBy, int page = 1, int pageSize = 20);

        IEnumerable<TModel> Find<TOrderBy>(Func<TModel, bool> predicate, Func<TModel, TOrderBy> orderBy);

        IEnumerable<TModel> FindPaging<TOrderBy>(Func<TModel, bool> predicate, Func<TModel, TOrderBy> orderBy, int page = 1, int pageSize = 20);

        TModel Add(TModel model);
        void AddAll(IEnumerable<TModel> models);

        void Remove(TModel model);
        void RemoveAll(IEnumerable<TModel> models);


    }
}
