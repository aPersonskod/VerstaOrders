using Microsoft.AspNetCore.Mvc;
using VerstaOrders.Model;
using VerstaOrders.Services;

namespace VerstaOrders.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = await orderService.GetAllOrders();
        return Ok(orders);
    }

    [HttpGet("{orderId:guid}")]
    public async Task<IActionResult> Get(Guid orderId)
    {
        var order = await orderService.GetOrder(orderId);
        return Ok(order);
    }

    [HttpPut]
    public async Task<IActionResult> CreateOrder([FromBody]CreateOrderDto orderDto)
    {
        var createdOrder = await orderService.CreateOrder(orderDto);
        return Ok(createdOrder);
    }
}