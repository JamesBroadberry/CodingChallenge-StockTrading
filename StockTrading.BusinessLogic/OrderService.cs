using System;
using System.Collections.Generic;
using StockTrading.DataAccess.DataAccess;
using StockTrading.DataAccess.Models;

namespace StockTrading.BusinessLogic
{
    public class OrderService : IOrderService
    {
        private readonly IOrderContext _orderContext;
        private readonly ICompanyContext _companyContext;

        public OrderService(IOrderContext orderContext, ICompanyContext companyContext)
        {
            _orderContext = orderContext;
            _companyContext = companyContext;
        }

        public Order GetOrderById(Guid orderId)
        {
            return _orderContext.GetOrderById(orderId);
        }

        public IEnumerable<Order> GetOutstandingOrders()
        {
            return _orderContext.GetProcessingOrders();
        }

        public Order AddOrder(Order orderToAdd)
        {
            // TODO: Add business logic for updating orders
            if (_companyContext.GetCompany(orderToAdd.CompanySymbol) == null)
            {
                return null;
            }
            return _orderContext.AddOrder(orderToAdd);
        }
    }
}
