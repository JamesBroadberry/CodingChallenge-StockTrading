using StockTrading.DataAccess.Models;

namespace StockTrading.API.Model
{
    /// <summary>
    /// The payload containing information about the order to create
    /// </summary>
    public class CreateOrderRequest
    {
        /// <summary>
        /// The symbol representing the company that the order is for
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// The minimum price that the order should be considered for
        /// </summary>
        public decimal MinOrderPrice { get; set; }

        /// <summary>
        /// The maximum price that the order should be considered for
        /// </summary>
        public decimal MaxOrderPrice { get; set; }

        /// <summary>
        /// The number of shares to place the order for
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The side that the order is for (either to buy or sell)
        /// </summary>
        public OrderType OrderType { get; set; }
    }
}
