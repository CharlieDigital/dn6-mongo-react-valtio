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

    /// <summary>
    /// Joins the specified connection ID to the group.  The group name is arbitrary and could represent
    /// any scope; it can be an ID, a location, etc.
    /// </summary>
    /// <param name="connectionId">The ID of the connection to join to the group.</param>
    /// <param name="group">The name of the group to join the connection to.</param>
    [HttpGet("/api/notifications/join/{connectionId}/{group}", Name = nameof(JoinGroup))]
    public async Task JoinGroup(
        string connectionId,
        string group)
    {
        _logger.LogInformation($"Joining group: {group}");
        await _hub.Groups.AddToGroupAsync(connectionId, group);
    }

    /// <summary>
    /// Notifies the taret group with a message.
    /// </summary>
    /// <param name="group">The name of the group to send the message to.</param>
    /// <param name="message">The message to broadcast to the group.</param>
    [HttpGet("/api/notifications/notify/{group}/{message}", Name = nameof(NotifyGroup))]
    public async Task NotifyGroup(
        string group,
        string message)
    {
        _logger.LogInformation($"Joining group: {group}");
        await _hub.Clients.Group(group).Send(message);
    }
}