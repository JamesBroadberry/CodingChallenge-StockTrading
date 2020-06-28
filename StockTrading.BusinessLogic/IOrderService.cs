using System;
using System.Collections.Generic;
using StockTrading.DataAccess.Models;

namespace StockTrading.BusinessLogic
{
    /// <summary>
    /// The service to modify orders
    /// </summary>
    public interface IOrderService
    {
        /// <summary>
        /// Get an order by it's specific ID
        /// </summary>
        /// <param name="orderId">The order ID to find</param>
        /// <returns>If found, returns the order else returns null</returns>
        Order GetOrderById(Guid orderId);

        /// <summary>
        /// Get all orders which haven't been processed
        /// </summary>
        /// <returns>An IEnumerable of a all orders which haven't been processed</returns>
        IEnumerable<Order> GetOutstandingOrders();
        
        /// <summary>
        /// Create a new order
        /// This will try to match previous orders and modify those if the incoming order can be filled
        /// </summary>
        /// <param name="orderToAdd">The order to be created</param>
        /// <returns>The created order</returns>
        Order AddOrder(Order orderToAdd);

    }
}
