
namespace StockTrading.API.Model
{
    /// <summary>
    /// The payload containing how many shares and at what price to issue
    /// </summary>
    public class IssueSharesRequest
    {
        /// <summary>
        /// The number of shares to issue
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// The price which the shares will cost
        /// </summary>
        public decimal Price { get; set; }
    }
}
