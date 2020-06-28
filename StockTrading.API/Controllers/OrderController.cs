using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using StockTrading.BusinessLogic;
using StockTrading.API.Model;
using StockTrading.DataAccess.Models;

namespace StockTrading.API.Controllers
{
    /// <summary>
    /// The controller for all order related operations
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        /// <summary>
        /// The controller for all order related operations
        /// </summary>
        /// <param name="orderService">The order service to use when fulfilling API requests</param>
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Get all outstanding (not processed) orders
        /// </summary>
        /// <returns>A list of all outstanding orders</returns>
        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _orderService.GetOutstandingOrders();
        }

        /// <summary>
        /// Get information about a specific order
        /// </summary>
        /// <param name="orderId">The order ID to look up</param>
        /// <returns>If the order exists, returns the order else returns null</returns>
        [HttpGet("{orderId}")]
        public Order GetById(Guid orderId)
        {
            var orderToReturn = _orderService.GetOrderById(orderId);

            if (orderToReturn == null)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                return null;
            }

            return orderToReturn;
        }

        /// <summary>
        /// Create a new order
        /// </summary>
        /// <param name="request">The payload containing information about the order to create</param>
        /// <returns>The created order</returns>
        [HttpPost]
        public Order Post([FromBody] CreateOrderRequest request)
        {
            var orderToPlace = new Order(request.Symbol, request.MinOrderPrice, request.MaxOrderPrice, request.Quantity, request.OrderType);

            var addedOrder = _orderService.AddOrder(orderToPlace);
            if (addedOrder == null)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
            }

            return addedOrder;
        }

    }
}
