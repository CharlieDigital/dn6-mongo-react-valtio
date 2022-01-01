namespace Api.DataAccess.Support;

/// <summary>
/// Instance that implements the IDataServices contract.
/// </summary>
public class DataServices : IDataServices
{
    private readonly MongoDbContext _context;

    /// <summary>
    /// Injection constructor.
    /// </summary>
    /// <param name="context">The injected instance of the MongoDbContext.</param>
    public DataServices(MongoDbContext context)
    {
        this._context = context;
    }

    /// <summary>
    /// Repository for interfacing with the Company collection.
    /// </summary>
    /// <returns>The repository instance.</returns>
    public CompanyRepository Companies => new CompanyRepository(this._context);

    /// <summary>
    /// Repository for interfacing with the Employee collection.
    /// </summary>
    /// <returns>The repository instance.</returns>
    public EmployeeRepository Employees => new EmployeeRepository(this._context);
}