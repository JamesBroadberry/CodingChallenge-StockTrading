using System.Collections.Generic;
using StockTrading.DataAccess.DataAccess;
using StockTrading.DataAccess.Models;

namespace StockTrading.BusinessLogic
{
    ///<inheritdoc />
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyContext _companyContext;
        private readonly IOrderContext _orderContext;
        public CompanyService(ICompanyContext companyContext, IOrderContext orderContext)
        {
            _companyContext = companyContext;
            _orderContext = orderContext;
        }

        ///<inheritdoc />
        public IEnumerable<Company> GetCompanies()
        {
            return _companyContext.GetCompanies();
        }

        ///<inheritdoc />
        public Company GetCompany(string companySymbol)
        {
            return _companyContext.GetCompany(companySymbol);
        }

        ///<inheritdoc />
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

        ///<inheritdoc />
        public Company AddCompany(Company company)
        {
            return _companyContext.AddCompany(company);
        }
    }
}
