using Microsoft.AspNetCore.Mvc;
using NotificationApi.Models;
using NotificationApi.Services;

namespace NotificationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly NotificationService notificationsService;

    public NotificationController(NotificationService service)
    {
        notificationsService = service;
    }

    [HttpGet]
    public async Task<List<Notification>> Get()
    {
        return await notificationsService.GetAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Notification>> Get(string id)
    {
        var notification = await notificationsService.GetAsync(id);

        if (notification is null)
            return NotFound();

        return notification;
    }

    [HttpPost]
    public async Task<ActionResult<Notification>> Post(string description)
    {
        await notificationsService.CreateAsync(description);
        return Ok(description);
    }
}
