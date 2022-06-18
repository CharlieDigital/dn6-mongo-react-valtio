public class AzureSettings
{
    /// <summary>
    /// The connection string to access SignalR.
    /// </summary>
    public string SignalRConnection { get; set; } = "";

    /// <summary>
    /// Convenience method to test if the SignalR endpoint is configured.
    /// </summary>
    /// <returns>True when configured to a non-empty value.</returns>
    public bool IsSignalRConfigured()
    {
        return !string.IsNullOrEmpty(SignalRConnection);
    }
}