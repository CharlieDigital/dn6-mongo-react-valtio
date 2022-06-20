using System.Security.Claims;
using Microsoft.AspNetCore.SignalR;
/// <summary>
/// This interface allows for a strongly typed hub.
/// See: https://docs.microsoft.com/en-us/aspnet/core/signalr/hubcontext?view=aspnetcore-6.0#inject-a-strongly-typed-hubcontext
/// </summary>
public interface INotificationHub
{
    /// <summary>
    /// This method will send a message to all connected clients.
    /// </summary>
    Task Send(string message);

    /// <summary>
    /// Send a message to a specific group.
    /// </summary>
    /// <param name="groupName">The group to target the notification to.</param>
    /// <param name="message">The message to send to the group.</param>
    Task NotifyGroup(string groupName, string message);

    /// <summary>
    /// Send a message to a specific user.
    /// </summary>
    /// <param name="user">The user identifier.</param>
    /// <param name="message">The message to send to the user.</param>
    Task NotifyUser(string user, string message);
}

/// <summary>
/// This class represents the hub.
/// </summary>
[Authorize]
public class NotificationHub : Hub<INotificationHub>
{
    /// <summary>
    /// This method will send a message to all connected clients.
    /// </summary>
    /// <param name="message">The string message to send.</param>
    public Task Send(string message)
    {
        return Clients.All.Send(message);
    }

    /// <summary>
    /// Send a message to a specific group.
    /// </summary>
    /// <param name="groupName">The group to target the notification to.</param>
    /// <param name="message">The message to send to the group.</param>
    public Task NotifyGroup(string groupName, string message)
    {
        return Clients.Group(groupName).Send(message);
    }

    /// <summary>
    /// Send a message to a specific user.
    /// </summary>
    /// <param name="user">The user identifier.</param>
    /// <param name="message">The message to send to the user.</param>
    public Task NotifyUser(string user, string message)
    {
        return Clients.User(user).Send(message);
    }
}