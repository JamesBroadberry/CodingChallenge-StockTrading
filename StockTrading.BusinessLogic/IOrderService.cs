using System;
using System.Collections.Generic;
using StockTrading.DataAccess.Models;

namespace StockTrading.BusinessLogic
{
    public interface IOrderService
    {
        Order GetOrderById(Guid orderId);
        IEnumerable<Order> GetOutstandingOrders();
        Order AddOrder(Order orderToAdd);

    }
}
