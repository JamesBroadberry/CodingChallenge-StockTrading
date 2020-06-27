using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using StockTrading.BusinessLogic;
using StockTrading.API.Model;
using StockTrading.DataAccess.Models;

namespace StockTrading.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<Order> Get()
        {
            return _orderService.GetOutstandingOrders();
        }

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
