namespace Api.DataAccess;

/// <summary>
/// Repository for interfacing with Company entities.
/// </summary>
public class CompanyRepository : RepositoryBase<Company>
{
    public CompanyRepository(MongoDbContext context) : base(context)
    {
    }
}