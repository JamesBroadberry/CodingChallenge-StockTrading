using System.Collections.Generic;
using StockTrading.DataAccess.Models;

namespace StockTrading.DataAccess.DataAccess
{
    public interface ICompanyContext
    {
        IEnumerable<Company> GetCompanies();
        Company GetCompany(string companySymbol);
        Company AddCompany(Company company);
    }
}
