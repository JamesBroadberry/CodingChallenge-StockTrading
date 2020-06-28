using System;
using System.Collections.Generic;
using StockTrading.DataAccess.Models;

namespace StockTrading.DataAccess.DataAccess
{
    /// <summary>
    /// The data access for order related operations
    /// </summary>
    public interface IOrderContext
    {
        /// <summary>
        /// Retrieve an order by it's ID
        /// </summary>
        /// <param name="orderId">The order ID to look up</param>
        /// <returns>The order if found, else null</returns>
        Order GetOrderById(Guid orderId);

        /// <summary>
        /// Get all orders which haven't been fully processed
        /// </summary>
        /// <returns>An IEnumerable of all orders which haven't been processed</returns>
        IEnumerable<Order> GetOutstandingOrders();

        /// <summary>
        /// Add an order to the data store
        /// </summary>
        /// <param name="orderToAdd">The order to add</param>
        /// <returns>The order after it's been added</returns>
        Order AddOrder(Order orderToAdd);

        /// <summary>
        /// Update a specific order using it's ID
        /// </summary>
        /// <param name="orderToUpdate">The order to update by ID</param>
        /// <returns>The updated order if found, else null</returns>
        Order UpdateOrder(Order orderToUpdate);

    }
}
