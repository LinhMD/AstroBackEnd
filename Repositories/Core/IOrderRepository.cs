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
    }
}
