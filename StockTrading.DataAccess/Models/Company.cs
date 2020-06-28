namespace StockTrading.DataAccess.Models
{
    /// <summary>
    /// The data model representing a company
    /// </summary>
    public class Company
    {
        public Company(string symbol)
        {
            Symbol = symbol;
        }

        /// <summary>
        /// The symbol that represents a company
        /// </summary>
        public string Symbol { get; }
    }
}
