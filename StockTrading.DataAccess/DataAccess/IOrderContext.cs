using System;
using System.Collections.Generic;
using StockTrading.DataAccess.Models;

namespace StockTrading.DataAccess.DataAccess
{
    public interface IOrderContext
    {
        Order GetOrderById(Guid orderId);
        IEnumerable<Order> GetProcessingOrders();
        Order AddOrder(Order orderToAdd);
        Order UpdateOrder(Order orderToUpdate);

    }
}
