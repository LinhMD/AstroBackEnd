using AstroBackEnd.Data;
using AstroBackEnd.Models;
using AstroBackEnd.Repositories.Core;
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
    }
}
