using System;
using System.Collections.Generic;
using System.Linq;
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
            if (_companyContext.GetCompany(orderToAdd.CompanySymbol) == null)
            {
                return null;
            }

            var outstandingOrdersForSymbolInPriceRange = _orderContext.GetProcessingOrders()
                .Where(x => x.CompanySymbol == orderToAdd.CompanySymbol) // Match orders for company which matches
                .Where(x => x.OrderType != orderToAdd.OrderType) // Only show buy orders if current is sell and vice versa
                .Where(x => x.MinOrderPrice <= orderToAdd.MaxOrderPrice && x.MaxOrderPrice >= orderToAdd.MinOrderPrice) // Price ranges overlap
                .OrderBy(x => x.Created); // Fill oldest orders first

            foreach (var outstandingOrder in outstandingOrdersForSymbolInPriceRange)
            {

                // Find the lower value of quantity remaining
                var numberOfShares = Math.Min(orderToAdd.QuantityRemaining, outstandingOrder.QuantityRemaining);

                // Update the remaining quantity on both orders
                outstandingOrder.QuantityRemaining -= numberOfShares;
                orderToAdd.QuantityRemaining -= numberOfShares;

                // Update existing order
                _orderContext.UpdateOrder(outstandingOrder);

                // Exit if no more can be filled
                if (orderToAdd.QuantityRemaining == 0)
                {
                    break;
                }
            }

            return _orderContext.AddOrder(orderToAdd);
        }
    }
}
