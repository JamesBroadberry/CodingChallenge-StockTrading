using System.Collections.Generic;
using System.Linq;
using StockTrading.DataAccess.Models;

namespace StockTrading.DataAccess.DataAccess
{
    ///<inheritdoc />
    public class CompanyContext : ICompanyContext
    {
        private readonly List<Company> _companies = new List<Company>();

        ///<inheritdoc />
        public IEnumerable<Company> GetCompanies()
        {
            return _companies;
        }

        ///<inheritdoc />
        public Company GetCompany(string companySymbol)
        {
            return _companies.FirstOrDefault(x => x.Symbol == companySymbol);
        }

        ///<inheritdoc />
        public Company AddCompany(Company company)
        {
            _companies.Add(company);
            return company;
        }
    }
}
