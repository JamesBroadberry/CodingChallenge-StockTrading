using System;

namespace StockTrading.DataAccess.Models
{
    /// <summary>
    /// The data model representing an order
    /// </summary>
    public class Order
    {
        public Order(string companySymbol, decimal minOrderPrice, decimal maxOrderPrice, int quantity, OrderType orderType)
        {
            CompanySymbol = companySymbol;
            MinOrderPrice = minOrderPrice;
            MaxOrderPrice = maxOrderPrice;
            Quantity = quantity;
            QuantityRemaining = quantity;
            OrderType = orderType;
        }

        /// <summary>
        /// The identifier of the order
        /// </summary>
        public Guid Id { get; internal set; }

        /// <summary>
        /// The time which the order was created
        /// </summary>
        public DateTime Created { get; internal set; }

        /// <summary>
        /// The symbol of the company which this order is for
        /// </summary>
        public string CompanySymbol { get; }

        /// <summary>
        /// The minimum price the order will match for
        /// </summary>
        public decimal MinOrderPrice { get; }

        /// <summary>
        /// The maximum price the order will match for
        /// </summary>
        public decimal MaxOrderPrice { get; }

        /// <summary>
        /// The initial number of shares included in the order
        /// </summary>
        public int Quantity { get; }

        /// <summary>
        /// The remaining number of shares the order needs to be fulfilled
        /// </summary>
        public int QuantityRemaining { get; set; }

        /// <summary>
        /// The side the order represents (buy or sell)
        /// </summary>
        public OrderType OrderType { get; }

        /// <summary>
        /// Whether the order has been processed or is still processing
        /// </summary>
        public OrderStatus OrderStatus => QuantityRemaining == 0 ? OrderStatus.Processed : OrderStatus.Processing;
    }
}
