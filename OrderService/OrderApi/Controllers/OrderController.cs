using Microsoft.AspNetCore.Mvc;
using InventoryApi.Interfaces;
using InventoryApi.Models;

namespace InventoryApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService orderService;
    private readonly IRabbitMqPublisher publisher;

    public OrderController(IOrderService orderService, IRabbitMqPublisher publisher)
    {
        this.orderService = orderService;
        this.publisher = publisher;
    }

    [HttpGet]
    public async Task<ActionResult<List<Order>>> Get()
    {
        var orders = await orderService.GetAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> Get(Guid id)
    {
        var order = await orderService.GetAsync(id);
        if (order is null)
            return BadRequest();
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult<Order>> Post([FromBody] List<OrderItem> orderItems)
    {
        var order = await orderService.CreateAsync(orderItems);
        if (order is null)
            return BadRequest();
        publisher.SendMessage($"Created Order No {order.Id} at {order.CreatedAt}");
        return Ok(order);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var order = await orderService.DeleteAsync(id);
        if (order is null)
            return BadRequest();
        publisher.SendMessage($"Deleted Order No {order.Id} at {DateTime.UtcNow}");
        return Ok(order);
    }
}
