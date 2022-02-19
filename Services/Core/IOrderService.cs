using AstroBackEnd.Models;
using AstroBackEnd.RequestModels.OrderRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Core
{
    public interface IOrderService
    {
        public IEnumerable<Order> GetAllOrders();

        public Order GetOrder(int id);

        public IEnumerable<Order> FindOrder(FindOrderRequest request);

        public Order CreateOrder(CreateOrderRequest request);

        public Order UpdateOrder(int id, CreateOrderRequest request);

        public void DeleteOrder(int id);
    }
}
