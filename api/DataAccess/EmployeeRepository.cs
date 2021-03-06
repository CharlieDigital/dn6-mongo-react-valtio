namespace Api.DataAccess;

/// <summary>
/// Repository for interfacing with Employee entities.
/// </summary>
public class EmployeeRepository : RepositoryBase<Employee>
{
    /// <summary>
    /// Creates an instance of the repository for the MongoDB database context.
    /// </summary>
    /// <param name="context">The MongoDbContext with the connection information passed to the base class.</param>
    /// <returns>An instance of the repository.</returns>
    public EmployeeRepository(MongoDbContext context) : base(context)
    {

    }

    /// <summary>
    /// Gets a listing of employees for a given company ID.
    /// </summary>
    /// <param name="companyId">The ID of the company to retrieve employees for.</param>
    /// <param name="start">The starting index of companies to retrieve.</param>
    /// <param name="pageSize">The number of companies to retrieve.</param>
    /// <returns>The specified number of companies starting from the specified start index sorted by title.</returns>
    public async virtual Task<IEnumerable<Employee>> GetByCompanyAsync(string companyId, int start, int pageSize)
    {
        return await base.GetList(start, pageSize, (e) => e.Company!.Id == companyId);
    }

    /// <summary>
    /// Deletes Employee entities for a given Company.
    /// </summary>
    /// <param name="companyId">The company that is being deleted.</param>
    /// <returns>The result of the delete opration.</returns>
    public virtual async Task<DeleteResult> DeleteByCompanyAsync(string companyId)
    {
        return await Collection.DeleteManyAsync<Employee>(e => e.Company!.Id == companyId);
    }
}