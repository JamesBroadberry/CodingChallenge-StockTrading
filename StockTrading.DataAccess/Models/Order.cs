using System;

namespace StockTrading.DataAccess.Models
{
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

        public Guid Id { get; internal set; }
        public DateTime Created { get; internal set; }
        public string CompanySymbol { get; set; }
        public decimal MinOrderPrice { get; set; }
        public decimal MaxOrderPrice { get; set; }
        public int Quantity { get; set; }
        public int QuantityRemaining { get; internal set; }
        public OrderType OrderType { get; set; }
        public OrderStatus OrderStatus { get; } = OrderStatus.Processing;
    }
}
