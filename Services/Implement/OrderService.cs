using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.OrderRequest;
using AstroBackEnd.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Implement
{
    public class OrderService : IOrderService, IDisposable
    {
        private readonly IUnitOfWork _work;

        public OrderService(IUnitOfWork work)
        {
            this._work = work;
        }
        public Order CreateOrder(CreateOrderRequest request)
        {
            User user = _work.Users.Get(request.UserId);
            if (user == null) throw new ArgumentException("User not found");

            var order = new Order()
            {
                OrderTime = request.OrderTime == null ? DateTime.Now : request.OrderTime,
                DeliveryAdress = request.DeliveryAdress,
                DeleveryPhone = request.DeleveryPhone,
                Status = (int)request.Status,
                UserId = request.UserId,
                TotalCost = 0D
            };


            return _work.Orders.Add(order);
        }

        public void DeleteOrder(int id)
        {
            Order order = _work.Orders.Get(id);

            if(order != null)
            {
                _work.Orders.Remove(order);
            }
            else
            {
                throw new ArgumentException("Order Not Found!!");
            }
        }

        public void Dispose()
        {
            this._work.Complete();
        }

        public IEnumerable<Order> FindOrder(FindOrderRequest request)
        {
            bool filter(Order order)
            {
                bool userCheck = request.UserId == null || order.UserId == request.UserId;

                bool statusCheck = request.Status == null || order.Status == request.Status;

                bool totalStartCheck = request.TotalCostStart == null || order.TotalCost >= request.TotalCostStart;
                bool totalEndCheck = request.TotalCostEnd == null || order.TotalCost >= request.TotalCostEnd;
                bool totalCostCheck = totalStartCheck && totalEndCheck;

                bool addressCheck = string.IsNullOrWhiteSpace(request.DeliveryAdress) || order.DeliveryAdress.Contains(request.DeliveryAdress);

                bool phoneCheck = string.IsNullOrWhiteSpace(request.DeleveryPhone) || order.DeleveryPhone.Contains(request.DeleveryPhone);

                return userCheck && statusCheck && totalCostCheck && addressCheck && phoneCheck;
            }
            PagingRequest paging = request.PagingRequest;

            if (paging == null || paging.SortBy == null)
            {
                return _work.Orders.FindPaging(filter, o => o.OrderTime, paging.Page, paging.PageSize);
            }
            else
            {
                return paging.SortBy switch
                {
                    "OrderTime"         => _work.Orders.FindPaging(filter, o => o.OrderTime, paging.Page, paging.PageSize),
                    "TotalCost"         => _work.Orders.FindPaging(filter, o => o.TotalCost, paging.Page, paging.PageSize),
                    "DeliveryAdress"    => _work.Orders.FindPaging(filter, o => o.DeliveryAdress, paging.Page, paging.PageSize),
                    "DeleveryPhone"     => _work.Orders.FindPaging(filter, o => o.DeleveryPhone, paging.Page, paging.PageSize),
                    _                   => _work.Orders.FindPaging(filter, o => o.OrderTime, paging.Page, paging.PageSize),
                };
            }
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _work.Orders.GetAll(o => o.OrderTime);
        }

        public Order GetOrder(int id)
        {
            Order order = _work.Orders.Get(id);

            if (order != null)
            {
                return order;
            }
            else
            {
                throw new ArgumentException("Order Not Found!!");
            }
        }

        public Order UpdateOrder(int id, CreateOrderRequest request)
        {
            User user = _work.Users.GetAllUserData(request.UserId);
            if (user == null) throw new ArgumentException("User not found");

            Order order = user.Orders.FirstOrDefault(o => o.Id == id);
            if(order == null) throw new ArgumentException("Order not found");

            order.OrderTime = request.OrderTime == null ? DateTime.Now : request.OrderTime;
            order.DeliveryAdress = request.DeliveryAdress == null? order.DeliveryAdress : request.DeliveryAdress;
            order.DeleveryPhone = request.DeleveryPhone == null? order.DeleveryPhone: request.DeleveryPhone;
            order.Status = (int)request.Status;
            order.TotalCost = order.OrderDetails.Sum(od => od.Price);

            return order;
        }
    }
}
