using AstroBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Repositories.Core
{
    public interface IOrderRepository : IRepository<Order>
    {
        public Order GetAllOrderInfo(int id);

        public IEnumerable<Order> FindWithAllInfo<TOrderBy>(Func<Order, bool> predicate, Func<Order, TOrderBy> orderBy, int page = 1, int pageSize = 20);
    }
}
