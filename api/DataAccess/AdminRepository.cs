namespace Api.DataAccess;

/// <summary>
/// Database Admin functions.
/// </summary>
public class AdminRepository : RepositoryBase<Company>
{
    /// <summary>
    /// Creates an instance of the repository for the MongoDB database context.
    /// </summary>
    /// <param name="context">The MongoDbContext with the connection information passed to the base class.</param>
    /// <returns>An instance of the repository.</returns>
    public AdminRepository(MongoDbContext context) : base(context)
    {

    }

    /// <summary>
    /// Drops the two named collections.
    /// </summary>
    public async Task DropCollectionsAsync()
    {
        await Context.Database.DropCollectionAsync(nameof(Company));
        await Context.Database.DropCollectionAsync(nameof(Employee));
    }
}