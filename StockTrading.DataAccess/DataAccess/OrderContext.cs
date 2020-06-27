using System;
using System.Collections.Generic;
using System.Linq;
using StockTrading.DataAccess.Models;

namespace StockTrading.DataAccess.DataAccess
{
    public class OrderContext : IOrderContext
    {
        private readonly List<Order> _orders = new List<Order>();

        public Order GetOrderById(Guid orderId)
        {
            return _orders.FirstOrDefault(x => x.Id == orderId);
        }

        public IEnumerable<Order> GetProcessingOrders()
        {
            return _orders.Where(x => x.OrderStatus == OrderStatus.Processing);
        }

        public Order AddOrder(Order orderToAdd)
        {
            orderToAdd.Id = Guid.NewGuid();
            orderToAdd.Created = DateTime.UtcNow;
            _orders.Add(orderToAdd);
            return orderToAdd;
        }

        public Order UpdateOrder(Order orderToUpdate)
        {
            var foundOrderIndex = _orders.FindIndex(x => x.Id == orderToUpdate.Id);
            if (foundOrderIndex == -1)
            {
                return null;
            }

            _orders[foundOrderIndex] = orderToUpdate;
            return _orders[foundOrderIndex];
        }
    }
}
