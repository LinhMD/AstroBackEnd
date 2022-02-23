using AstroBackEnd.Models;
using AstroBackEnd.Repositories;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.OrderDetailRequest;
using AstroBackEnd.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstroBackEnd.Services.Implement
{
    public class OrderDetailService : IOrderDetailService, IDisposable
    {
        private readonly IUnitOfWork _work;

        public OrderDetailService(IUnitOfWork work)
        {
            _work = work;
        }
        public void Dispose()
        {
            this._work.Complete();
        }

        public OrderDetail CreateOrderDetail(OrderDetailCreateRequest request)
        {
            var order = _work.Orders.GetAllOrderInfo(request.OrderId);
            if (order == null) throw new ArgumentException("Order ID not found");

            var product = _work.Products.Get(request.ProductId);

            if (product == null) throw new ArgumentException("Product ID not found");

            if(product.MasterProduct == null) throw new ArgumentException("Product Must be a Product Variation");

            var detail = new OrderDetail()
            {
                OrderId = order.Id,
                ProductId = product.Id,
                Quantity = request.Quantity,
                TotalPrice = product.Price == null ? 0D : (double) product.Price * request.Quantity
            };

            return _work.OrderDetails.Add(detail);
        }

        public void DeleteOrderDetail(int id)
        {
            this._work.OrderDetails.Remove(GetOrderDetail(id));
        }

        

        public IEnumerable<OrderDetail> FindOrderDetail(FindOrderDetailRequest request)
        {

            Func<OrderDetail, bool> filter = detail =>
            {
                bool checkID = request.OrderId == null || detail.OrderId == request.OrderId;

                bool checkProductName = request.ProductName == null || _work.Products.Get(detail.ProductId).Name.Contains(request.ProductName);

                bool checkTotalPriceStart = request.TotalPriceStart == null || detail.TotalPrice >= request.TotalPriceStart;
                bool checkTotalPriceEnd = request.TotalPriceEnd == null || detail.TotalPrice <= request.TotalPriceEnd;

                bool checkQuatityStart = request.QuantityStart == null || detail.Quantity >= request.QuantityStart;
                bool checkQuatityEnd = request.QuantityEnd == null || detail.Quantity <= request.QuantityEnd;

                bool checkReviewMess = !string.IsNullOrWhiteSpace(request.ReviewMessage) ? 
                                        detail.ReviewMessage == null ? false : detail.ReviewMessage.Contains(request.ReviewMessage)  : true;

                bool checkReviewDateStart = request.ReviewDateStart != null ? detail.ReviewDate == null ? false : detail.ReviewDate >= request.ReviewDateStart : true;
                bool checkReviewDateEnd = request.ReviewDateEnd != null ? detail.ReviewDate == null ? false : detail.ReviewDate <= request.ReviewDateEnd : true;

                return checkID 
                        && checkProductName
                        && checkTotalPriceStart 
                        &&  checkTotalPriceEnd 
                        && checkQuatityStart
                        && checkQuatityEnd
                        && checkReviewMess
                        && checkReviewDateStart
                        && checkReviewDateEnd;
            };

            PagingRequest paging = request.Paging;

            if (paging == null || paging.SortBy == null)
            {
                return _work.OrderDetails.Find(filter, o => o.Id);
            }
            else
            {
                return paging.SortBy switch
                {
                    "Quantity" => _work.OrderDetails.FindPaging(filter, o => o.Quantity, paging.Page, paging.PageSize),
                    "ReviewDate" => _work.OrderDetails.FindPaging(filter, o => o.ReviewDate, paging.Page, paging.PageSize),
                    "TotalPrice" => _work.OrderDetails.FindPaging(filter, o => o.TotalPrice, paging.Page, paging.PageSize),
                    _ => _work.OrderDetails.FindPaging(filter, o => o.Id, paging.Page, paging.PageSize)
                };
            }
        }

        public IEnumerable<OrderDetail> GetAllOrderDetails()
        {
            return this._work.OrderDetails.GetAll(d => d.Id);
        }

        public OrderDetail GetOrderDetail(int id)
        {
            var detail = this._work.OrderDetails.Get(id);

            if(detail == null) throw new ArgumentException("Order ID not found");

            return detail;
        }

        public OrderDetail UpdateOrderDetail(int id, OrderDetailUpdateRequest request)
        {
            OrderDetail detail = this.GetOrderDetail(id);

            if (request.Quantity != null) 
            {
                detail.Quantity = (int)request.Quantity;

                var product = _work.Products.Get(detail.ProductId);
                if (product == null) throw new ArgumentException("Product ID not found");

                detail.TotalPrice = product.Price == null? 0D : (int)product.Price * detail.Quantity;
            }

            if (!string.IsNullOrWhiteSpace(request.ReviewMessage))
            {
                detail.ReviewMessage = request.ReviewMessage;
            }

            if (request.ReviewDate != null)
            {
                detail.ReviewDate = request.ReviewDate;
            }

            return detail;
        }
    }
}
