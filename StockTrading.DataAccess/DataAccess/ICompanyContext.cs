using System.Collections.Generic;
using StockTrading.DataAccess.Models;

namespace StockTrading.DataAccess.DataAccess
{
    /// <summary>
    /// The data access for order related operations
    /// </summary>
    public interface ICompanyContext
    {
        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns>An IEnumerable of all companies</returns>
        IEnumerable<Company> GetCompanies();

        /// <summary>
        /// Retrieve a specific company by it's symbol
        /// </summary>
        /// <param name="companySymbol">The symbol to look the company up by</param>
        /// <returns>The company if found, else null</returns>
        Company GetCompany(string companySymbol);

        /// <summary>
        /// Add a company to the data store
        /// </summary>
        /// <param name="company">The company to add</param>
        /// <returns>The company after it's been added</returns>
        Company AddCompany(Company company);
    }
}
