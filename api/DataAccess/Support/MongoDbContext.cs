

namespace Api.DataAccess.Support;

/// <summary>
/// Singleton instance for accessing the MongoDB connection.  Per the Mongo documentation, the
/// client instance is threadsafe and should be stored in a global context for re-use.  See:
/// http://mongodb.github.io/mongo-csharp-driver/2.10/reference/driver/connecting/#re-use-2
/// </summary>
public class MongoDbContext
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    /// <summary>
    /// The singleton instance of the MongoDB client.
    /// </summary>
    public IMongoClient Client => _client;

    /// <summary>
    /// The singleton instance of the MongoDB database.
    /// </summary>
    public IMongoDatabase Database => _database;

    /// <summary>
    /// Injection constructor that is initialized by the DI container.
    /// See the configuration options pattern docs:
    /// https://docs.microsoft.com/en-us/dotnet/core/extensions/options
    /// </summary>
    /// <param name="settings">The connection settings as configured in appsettings.json</param>
    public MongoDbContext(IOptions<MongoDbConnectionSettings> options)
    {
        MongoDbConnectionSettings settings = options.Value;

        Log.Information($"Connecting to database: {settings.DatabaseName}");
        Log.Information($"Connecting to with string: {settings.ConnectionString.Substring(0, 20)}*****");

        _client = new MongoClient(settings.ConnectionString);
        _database = _client.GetDatabase(settings.DatabaseName);
    }
}