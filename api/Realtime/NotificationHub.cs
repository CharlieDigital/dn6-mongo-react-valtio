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
}