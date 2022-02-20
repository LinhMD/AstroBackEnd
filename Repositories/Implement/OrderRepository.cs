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
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }

        public Order GetAllOrderInfo(int id)
        {
            return AstroData.Orders.Include("OrderDetails").FirstOrDefault(o => o.Id == id);
        }

        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }

    }
}
