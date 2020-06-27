using System.Collections.Generic;
using System.Linq;
using StockTrading.DataAccess.Models;

namespace StockTrading.DataAccess.DataAccess
{
    public class CompanyContext : ICompanyContext
    {
        private readonly List<Company> _companies = new List<Company>();

        public IEnumerable<Company> GetCompanies()
        {
            return _companies;
        }

        public Company GetCompany(string companySymbol)
        {
            return _companies.FirstOrDefault(x => x.Symbol == companySymbol);
        }

        public Company AddCompany(Company company)
        {
            _companies.Add(company);
            return company;
        }
    }
}
