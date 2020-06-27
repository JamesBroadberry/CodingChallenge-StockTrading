using System.Collections.Generic;
using StockTrading.DataAccess.DataAccess;
using StockTrading.DataAccess.Models;

namespace StockTrading.BusinessLogic
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyContext _companyContext;
        private readonly IOrderContext _orderContext;
        public CompanyService(ICompanyContext companyContext, IOrderContext orderContext)
        {
            _companyContext = companyContext;
            _orderContext = orderContext;
        }

        public IEnumerable<Company> GetCompanies()
        {
            return _companyContext.GetCompanies();
        }

        public Company GetCompany(string companySymbol)
        {
            return _companyContext.GetCompany(companySymbol);
        }

        public Order IssueShares(string companySymbol, int quantity, decimal price)
        {
            if (_companyContext.GetCompany(companySymbol) == null)
            {
                return null;
            }
            var orderToAdd = new Order(companySymbol, price, price, quantity, OrderType.Sell);

            var orderAdded = _orderContext.AddOrder(orderToAdd);

            return orderAdded;
        }

        public Company AddCompany(Company company)
        {
            return _companyContext.AddCompany(company);
        }
    }
}
