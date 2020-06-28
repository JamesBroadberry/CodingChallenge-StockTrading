using System.Collections.Generic;
using StockTrading.DataAccess.Models;

namespace StockTrading.BusinessLogic
{
    /// <summary>
    /// The service to modify company data
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Get all companies
        /// </summary>
        /// <returns>An IEnumerable of all of the companies</returns>
        IEnumerable<Company> GetCompanies();

        /// <summary>
        /// Get a specific company by it's symbol
        /// </summary>
        /// <param name="companySymbol">The company to look up</param>
        /// <returns>If found, returns the company else null</returns>
        Company GetCompany(string companySymbol);

        /// <summary>
        /// Issue shares for a company
        /// </summary>
        /// <param name="companySymbol">The company to issue shares for</param>
        /// <param name="quantity">The quantity of shares to issue</param>
        /// <param name="price">The price of the shares issued</param>
        /// <returns>The sell order created by issuing shares</returns>
        Order IssueShares(string companySymbol, int quantity, decimal price);

        /// <summary>
        /// Create a company
        /// </summary>
        /// <param name="company">The company to create</param>
        /// <returns>The created company</returns>
        Company AddCompany(Company company);
    }
}
