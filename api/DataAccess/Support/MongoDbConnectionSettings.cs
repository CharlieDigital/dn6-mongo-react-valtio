namespace Api.DataAccess.Support;

/// <summary>
/// This class is used to receive the configuration settins at startup.
/// </summary>
public class MongoDbConnectionSettings
{
    /// <summary>
    /// The connection string to the MongoDB database
    /// </summary>
    public string ConnectionString { get; set; } = null!;

    /// <summary>
    /// The name of the database.
    /// </summary>
    public string DatabaseName { get; set; } = null!;
}
