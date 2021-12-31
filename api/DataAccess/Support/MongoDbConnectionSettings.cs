namespace Api.DataAccess.Support;

public class MongoDbConnectionSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; }= null!;
}
