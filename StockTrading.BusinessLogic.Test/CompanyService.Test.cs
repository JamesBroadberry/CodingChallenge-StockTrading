using System.Linq;
using NUnit.Framework;
using StockTrading.DataAccess.DataAccess;
using StockTrading.DataAccess.Models;

namespace StockTrading.BusinessLogic.Test
{
    public class CompanyServiceTest
    {
        private ICompanyService _companyService;
        private IOrderService _orderService;

        [SetUp]
        public void Setup()
        {
            // TODO: Mock the company context and order context so it doesn't use a "real" database
            var companyContext = new CompanyContext();
            var orderContext = new OrderContext();
            _companyService = new CompanyService(companyContext, orderContext);
            _orderService = new OrderService(orderContext, companyContext);
        }

        [Test]
        public void WhenFirstInitialised_TheServiceHasNoCompanies()
        {
            // Act
            var numberOfCompanies = _companyService.GetCompanies().Count();

            // Assert
            Assert.AreEqual(0, numberOfCompanies);
        }

        [Test]
        public void WhenAddingACompany_TheNumberOfCompaniesReturnedIsIncreased()
        {
            // Arrange
            var numberOfCompaniesBefore = _companyService.GetCompanies().Count();

            // Act
            _companyService.AddCompany(new Company("TSTCMPNY"));

            // Assert
            var numberOfCompaniesAfter = _companyService.GetCompanies().Count();
            Assert.AreEqual(numberOfCompaniesBefore + 1, numberOfCompaniesAfter);
        }

        [Test]
        public void WhenAddingACompany_TheCompanyIsInsertedCorrectly()
        {
            // Arrange
            var companyToInsert = new Company("TSTCMPNY");

            // Act
            _companyService.AddCompany(companyToInsert);

            // Assert
            var companiesReturned = _companyService.GetCompanies();
            Assert.AreEqual(companyToInsert, companiesReturned.First());
        }

        [Test]
        public void WhenAddingACompany_ItCanBeRetrievedUsingItsSymbol()
        {
            // Arrange
            var companyToInsert = new Company("TSTCMPNY");

            // Act
            _companyService.AddCompany(companyToInsert);

            // Assert
            var companyReturned = _companyService.GetCompany(companyToInsert.Symbol);
            Assert.AreEqual(companyToInsert, companyReturned);
        }

        [Test]
        public void WhenACompanyIsFirstAdded_ThereAreNoOrdersForTheCompany()
        {
            // Arrange
            var company = new Company("TSTCMPNY");


            // Act
            _companyService.AddCompany(company);

            // Assert
            Assert.AreEqual(0, _orderService.GetOutstandingOrders().Count());
        }

        [Test]
        public void WhenIssuingSharesThroughACompany_AnOrderIsCreated()
        {
            // Arrange
            var company = new Company("TSTCMPNY");
            _companyService.AddCompany(company);

            // Act
            _companyService.IssueShares(company.Symbol, 10, 50m);

            // Assert
            Assert.AreEqual(1, _orderService.GetOutstandingOrders().Count());
        }

        [Test]
        public void WhenIssuingSharesThroughACompany_TheOrderMatchesTheSharesIssued()
        {
            // Arrange
            var company = new Company("TSTCMPNY");
            var numberOfSharesToIssue = 10;
            var priceOfSharesToIssue = 50m;
            _companyService.AddCompany(company);

            // Act
            _companyService.IssueShares(company.Symbol, numberOfSharesToIssue, priceOfSharesToIssue);

            // Assert
            var actualSharesIssued = _orderService.GetOutstandingOrders().First();
            Assert.AreEqual(numberOfSharesToIssue, actualSharesIssued.Quantity);
            Assert.AreEqual(numberOfSharesToIssue, actualSharesIssued.QuantityRemaining);
            Assert.AreEqual(priceOfSharesToIssue, actualSharesIssued.MinOrderPrice);
            Assert.AreEqual(priceOfSharesToIssue, actualSharesIssued.MaxOrderPrice);
            Assert.AreEqual(OrderStatus.Processing, actualSharesIssued.OrderStatus);
            Assert.AreEqual(OrderType.Sell, actualSharesIssued.OrderType);
        }

        [Test]
        public void WhenIssuingSharesForACompanyThatDoesntExist_AnOrderIsNotAdded()
        {
            // Act
            _companyService.IssueShares("NULL", 10, 50m);

            // Assert
            Assert.AreEqual(0, _orderService.GetOutstandingOrders().Count());
        }

        [Test]
        public void WhenIssuingSharesForACompanyThatDoesntExist_TheOrderReturnedIsNull()
        {
            // Act
            var order = _companyService.IssueShares("NULL", 10, 50m);

            // Assert
            Assert.AreEqual(null, order);
        }
    }
}