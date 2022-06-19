using Microsoft.AspNetCore.SignalR;

namespace Api.Controllers;

/// <summary>
/// API Contoller class for notifications..
/// </summary>
[ApiController]
[Authorize]
public class NotifcationsController : ControllerBase
{
    public readonly IHubContext<NotificationHub, INotificationHub> _hub;
    private readonly ILogger<EmployeeController> _logger;

    /// <summary>
    /// Injection constructor.
    /// </summary>
    public NotifcationsController(
        IHubContext<NotificationHub, INotificationHub> hub,
        ILogger<EmployeeController> logger)
    {
        _hub = hub;
        _logger = logger;
    }

    /// <summary>
    /// Echos a message to all connected clients.
    /// </summary>
    /// <param name="message">The message to send to all connected clients..</param>
    [HttpGet("/api/notifications/echo/{message}", Name = nameof(Echo))]
    public async Task Echo(string message)
    {
        _logger.LogInformation($"Echoing: {message}");
        await _hub.Clients.All.Send(message);
    }
}