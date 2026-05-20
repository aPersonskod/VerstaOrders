using Microsoft.AspNetCore.Mvc;

namespace VerstaOrders.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok("Order");
    }
}