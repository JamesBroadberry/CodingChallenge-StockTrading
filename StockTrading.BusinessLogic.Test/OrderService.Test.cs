using System;
using System.Linq;
using NUnit.Framework;
using StockTrading.DataAccess.DataAccess;
using StockTrading.DataAccess.Models;

namespace StockTrading.BusinessLogic.Test
{
    public class OrderServiceTest
    {
        private ICompanyService _companyService;
        private IOrderService _orderService;
        private Company _existingCompany;

        [SetUp]
        public void Setup()
        {
            // TODO: Mock the company context and order context so it doesn't use a "real" database
            var companyContext = new CompanyContext();
            var orderContext = new OrderContext();
            _companyService = new CompanyService(companyContext, orderContext);
            _orderService = new OrderService(orderContext, companyContext);


            _existingCompany = new Company("TSTCMPNY");
            _companyService.AddCompany(_existingCompany);

        }

        [Test]
        public void WhenFirstInitialised_TheServiceHasNoOutstandingOrders()
        {
            // Act
            var numberOfOrders = _orderService.GetOutstandingOrders().Count();

            // Assert
            Assert.AreEqual(0, numberOfOrders);
        }

        [Test]
        public void WhenAddingAnOrder_TheNumberOfOutstandingOrdersReturnedIsIncreased()
        {
            // Arrange
            var numberOfOrdersBefore = _orderService.GetOutstandingOrders().Count();

            // Act
            _orderService.AddOrder(new Order(_existingCompany.Symbol, 10m, 10m, 10, OrderType.Sell));

            // Assert
            var numberOfOrdersAfter = _orderService.GetOutstandingOrders().Count();
            Assert.AreEqual(numberOfOrdersBefore + 1, numberOfOrdersAfter);
        }

        [Test]
        public void WhenAddingAnOrder_ItIsReturnedCorrectlyWithAdditionalInformation()
        {
            // Arrange
            var orderToPlace = new Order(_existingCompany.Symbol, 50m, 50m, 50, OrderType.Sell);

            // Act
            _orderService.AddOrder(orderToPlace);

            // Assert
            var orderReturned = _orderService.GetOutstandingOrders().First();

            Assert.AreEqual(orderToPlace.CompanySymbol, orderReturned.CompanySymbol);
            Assert.AreEqual(orderToPlace.MinOrderPrice, orderReturned.MinOrderPrice);
            Assert.AreEqual(orderToPlace.MaxOrderPrice, orderReturned.MaxOrderPrice);
            Assert.AreEqual(orderToPlace.Quantity, orderReturned.Quantity);
            Assert.AreEqual(orderToPlace.OrderType, orderReturned.OrderType);

            Assert.AreEqual(orderToPlace.Quantity, orderReturned.QuantityRemaining);
            Assert.IsTrue(orderReturned.Id != null && orderReturned.Id != default);
            Assert.IsTrue(orderReturned.Created.Subtract(DateTime.UtcNow).Duration() <= TimeSpan.FromSeconds(5));

        }

        [Test]
        public void WhenAddingAnOrderForACompanyThatDoesNotExist_NullIsReturnedAndItsNotAdded()
        {
            // Arrange
            var orderToPlace = new Order("NULL", 50m, 50m, 50, OrderType.Sell);

            // Act
            var orderReturned = _orderService.AddOrder(orderToPlace);

            // Assert
            Assert.AreEqual(null, orderReturned);
            Assert.AreEqual(0, _orderService.GetOutstandingOrders().Count());

        }

        [Test]
        public void WhenGettingAnOrderByIdAndTheOrderDoesNotExist_NullIsReturned()
        {
            // Act
            var orderReturned = _orderService.GetOrderById(Guid.NewGuid());

            // Assert
            Assert.AreEqual(null, orderReturned);
        }

        [Test]
        public void WhenGettingAnOrderByIdAndTheOrderDoesExist_TheOrderIsReturned()
        {
            // Arrange
            var orderToPlace = new Order(_existingCompany.Symbol, 50m, 50m, 50, OrderType.Sell);
            var orderPlaced = _orderService.AddOrder(orderToPlace);

            // Act
            var orderReturned = _orderService.GetOrderById(orderPlaced.Id);

            // Assert
            Assert.AreEqual(orderToPlace, orderReturned);
        }

        [Test]
        public void AfterACompanyHasIssuedShares_PlacingABuyOrderWithAPriceOverlapBuysTheShares()
        {
            // Arrange
            var sharesIssueOrder = _companyService.IssueShares(_existingCompany.Symbol, 50, 50m);

            // Act
            var buySharesOrder = _orderService.AddOrder(new Order(_existingCompany.Symbol, 40m, 60m, 50, OrderType.Buy));

            // Assert
            Assert.AreEqual(0, _orderService.GetOutstandingOrders().Count());
            Assert.AreEqual(OrderStatus.Processed, _orderService.GetOrderById(sharesIssueOrder.Id).OrderStatus);
            Assert.AreEqual(OrderStatus.Processed, _orderService.GetOrderById(buySharesOrder.Id).OrderStatus);
            Assert.AreEqual(0, _orderService.GetOrderById(sharesIssueOrder.Id).QuantityRemaining);
            Assert.AreEqual(0, _orderService.GetOrderById(buySharesOrder.Id).QuantityRemaining);
        }

        [Test]
        public void WhenPartiallyBuyingASellOrder_TheQuantitiesAndStatuesAreUpdatedCorrectly()
        {
            // Arrange
            var sellOrder = _orderService.AddOrder(new Order(_existingCompany.Symbol, 60m, 60m, 100, OrderType.Sell));

            // Act
            var buyOrder = _orderService.AddOrder(new Order(_existingCompany.Symbol, 60m, 60m, 50, OrderType.Buy));

            // Assert
            Assert.AreEqual(1, _orderService.GetOutstandingOrders().Count());
            Assert.AreEqual(OrderStatus.Processing, _orderService.GetOrderById(sellOrder.Id).OrderStatus);
            Assert.AreEqual(OrderStatus.Processed, _orderService.GetOrderById(buyOrder.Id).OrderStatus);
            Assert.AreEqual(50, _orderService.GetOrderById(sellOrder.Id).QuantityRemaining);
            Assert.AreEqual(0, _orderService.GetOrderById(buyOrder.Id).QuantityRemaining);
        }

        [Test]
        public void WhenPartiallySellingABuyOrder_TheQuantitiesAndStatuesAreUpdatedCorrectly()
        {
            // Arrange
            var buyOrder = _orderService.AddOrder(new Order(_existingCompany.Symbol, 60m, 60m, 100, OrderType.Buy));

            // Act
            var sellOrder = _orderService.AddOrder(new Order(_existingCompany.Symbol, 60m, 60m, 50, OrderType.Sell));

            // Assert
            Assert.AreEqual(1, _orderService.GetOutstandingOrders().Count());
            Assert.AreEqual(OrderStatus.Processing, _orderService.GetOrderById(buyOrder.Id).OrderStatus);
            Assert.AreEqual(OrderStatus.Processed, _orderService.GetOrderById(sellOrder.Id).OrderStatus);
            Assert.AreEqual(50, _orderService.GetOrderById(buyOrder.Id).QuantityRemaining);
            Assert.AreEqual(0, _orderService.GetOrderById(sellOrder.Id).QuantityRemaining);
        }

        [Test]
        public void WhenABuyAndSimilarSellOrderAreCreatedWithPricesThatDontOverlap_TheyRemainDontMatch()
        {
            // Arrange
            var buyOrder = _orderService.AddOrder(new Order(_existingCompany.Symbol, 70m, 75m, 100, OrderType.Buy));

            // Act
            var sellOrder = _orderService.AddOrder(new Order(_existingCompany.Symbol, 60m, 65m, 100, OrderType.Sell));

            // Assert
            Assert.AreEqual(2, _orderService.GetOutstandingOrders().Count());
            Assert.AreEqual(OrderStatus.Processing, _orderService.GetOrderById(buyOrder.Id).OrderStatus);
            Assert.AreEqual(OrderStatus.Processing, _orderService.GetOrderById(sellOrder.Id).OrderStatus);
        }

        [Test]
        public void WhenThereAreMultipleSellOrdersAndABuyOrderMatches_TheOldestOrderIsPrioritised()
        {
            // Arrange
            var firstBuyOrder = _orderService.AddOrder(new Order(_existingCompany.Symbol, 70m, 75m, 100, OrderType.Buy));
            var secondBuyOrder = _orderService.AddOrder(new Order(_existingCompany.Symbol, 70m, 75m, 100, OrderType.Buy));

            // Act
            var sellOrder = _orderService.AddOrder(new Order(_existingCompany.Symbol, 73m, 73m, 150, OrderType.Sell));

            // Assert
            Assert.AreEqual(OrderStatus.Processed, _orderService.GetOrderById(firstBuyOrder.Id).OrderStatus);
            Assert.AreEqual(OrderStatus.Processing, _orderService.GetOrderById(secondBuyOrder.Id).OrderStatus);
            Assert.AreEqual(OrderStatus.Processed, _orderService.GetOrderById(sellOrder.Id).OrderStatus);
        }
    }
}
