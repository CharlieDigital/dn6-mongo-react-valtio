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
    /// Gets a Employee by ID
    /// </summary>
    /// <param name="id">The ID of the Employee to retrieve.</param>
    /// <returns>The Employee instance that matches the ID.</returns>
    [HttpGet("/api/notifications/echo/{message}", Name = nameof(Echo))]
    public async Task Echo(string message)
    {
        _logger.LogInformation($"Echoing: {message}");
        var employee = _hub.Clients.All.Send(message);
        await Task.CompletedTask;
    }
}