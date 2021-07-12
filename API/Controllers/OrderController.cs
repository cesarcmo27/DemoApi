using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Orders;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static Application.Orders.Create;

namespace API.Controllers
{
    public class OrderController : BaseController
    {
        private readonly ILogger<OrderController> _logger;

    public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetOrders()
        {
            _logger.LogInformation("Inicio GetOrders => "+ DateTime.Now);
            var result = await Mediator.Send(new List.Query());

           _logger.LogInformation("Fin GetOrders => "+ DateTime.Now);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            return Ok(await Mediator.Send(new Create.Command { Order = order }));
        }

    }
}