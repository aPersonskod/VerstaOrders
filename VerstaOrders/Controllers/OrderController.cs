using Microsoft.AspNetCore.Mvc;
using VerstaOrders.Model.Dto;
using VerstaOrders.Services;

namespace VerstaOrders.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IOrderService orderService) : ControllerBase
{
    [HttpGet("[action]")]
    public async Task<IActionResult> GetOrders()
    {
        var orders = orderService.GetAllOrders();
        return Ok(orders);
    }

    [HttpGet("{orderNumber}")]
    public async Task<IActionResult> Get(string orderNumber)
    {
        var order = await orderService.GetOrder(orderNumber);
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody]CreateOrderDto orderDto)
    {
        var createdOrder = await orderService.CreateOrder(orderDto);
        return Ok(createdOrder);
    }

    #region for test

    [HttpDelete("{townSender}")]
    public async Task<IActionResult> Delete(string townSender)
    {
        await orderService.DeleteOrderByTownSender(townSender);
        return Ok();
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> ClearTestSequences(int year, int month)
    {
        await orderService.ClearTestSequences(year, month);
        return Ok();
    }

    #endregion
    
}