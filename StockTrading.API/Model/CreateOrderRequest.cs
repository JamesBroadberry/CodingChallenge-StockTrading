using StockTrading.DataAccess.Models;

namespace StockTrading.API.Model
{
    public class CreateOrderRequest
    {
        public string Symbol { get; set; }
        public decimal MinOrderPrice { get; set; }
        public decimal MaxOrderPrice { get; set; }
        public int Quantity { get; set; }
        public OrderType OrderType { get; set; }
    }
}
