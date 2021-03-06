using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.OrderDetailRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Core
{
    public interface IOrderDetailService
    {

        public IEnumerable<OrderDetail> GetAllOrderDetails();

        public OrderDetail GetOrderDetail(int id);

        public IEnumerable<OrderDetail> FindOrderDetail(FindOrderDetailRequest request);

        public IEnumerable<OrderDetail> FindOrderDetail(FindOrderDetailRequest request, out int total);

        public OrderDetail CreateOrderDetail(OrderDetailCreateRequest request);

        public OrderDetail UpdateOrderDetail(int id, OrderDetailUpdateRequest request);

        public void DeleteOrderDetail(int id);
    }
}
