namespace Api.DataAccess;

/// <summary>
/// Repository for interfacing with Company entities.
/// </summary>
public class CompanyRepository : RepositoryBase<Company>
{
    /// <summary>
    /// Creates an instance of the repository for the MongoDB database context.
    /// </summary>
    /// <param name="context">The MongoDbContext with the connection information passed to the base class.</param>
    /// <returns>An instance of the repository.</returns>
    public CompanyRepository(MongoDbContext context) : base(context)
    {

    }

    /// <summary>
    /// Retrieves a "rich" object with the Company Employees.
    /// </summary>
    /// <param name="id">The ID of the Company to retrieve.</param>
    /// <returns>The Company with the Employees populated.</returns>
    public async Task<Company?> GetFullEntityAsync(string id)
    {
        // See: https://www.niceonecode.com/blog/64/left-join-in-mongodb-using-the-csharp-driver-and-linq
        IMongoCollection<Employee> employees =
            Context.Database.GetCollection<Employee>(nameof(Employee));

        var result = await Collection.AsQueryable()
            .Where(company => company.Id == id)
            .GroupJoin(
                employees.AsQueryable(),
                company => company.Id,
                employee => employee.Company!.Id,
                (c, companyEmployees) => new {
                    c, companyEmployees
                }).FirstOrDefaultAsync();

        /* Equivalent.
        var result = (from c in Collection.AsQueryable()
                    where c.Id == id
                    join e in employees.AsQueryable()
                    on c.Id equals e.Company.Id
                    into companyEmployees
                    select new {
                        c, companyEmployees
                    }).FirstOrDefault();
        */

        Company? company = result?.c;

        if(company != null)
        {
            company.Employees = result!
                .companyEmployees.Select(e => {
                    e.Company = null; // Null the company because we don't need it
                    return e;
                });
        }

        return company;
    }
}