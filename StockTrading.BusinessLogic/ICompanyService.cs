using System.Collections.Generic;
using StockTrading.DataAccess.Models;

namespace StockTrading.BusinessLogic
{
    public interface ICompanyService
    {
        IEnumerable<Company> GetCompanies();
        Company GetCompany(string companySymbol);
        Order IssueShares(string companySymbol, int quantity, decimal price);
        Company AddCompany(Company company);
    }
}
