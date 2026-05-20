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
        try
        {
            var orders = await orderService.GetAllOrders();
            return Ok(orders);
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get(Guid orderId)
    {
        try
        {
            var order = await orderService.GetOrder(orderId);
            return Ok(order);
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> CreateOrder([FromBody]Order order)
    {
        try
        {
            var createdOrder = await orderService.CreateOrder(order);
            return Ok(createdOrder);
        }
        catch (Exception e)
        {
            return BadRequest(new { error = e.Message });
        }
    }
}