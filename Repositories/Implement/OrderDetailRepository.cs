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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private AstroDataContext AstroData { get { return base._context as AstroDataContext; } }
        public OrderDetailRepository(AstroDataContext dataContext) : base(dataContext)
        {

        }

        public override IQueryable<OrderDetail> WithAllData()
        {
            return AstroData.OrderDetails.AsQueryable();
        }
    }
}
