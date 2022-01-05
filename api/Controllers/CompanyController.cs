namespace Api.Controllers;

/// <summary>
/// API Contoller class for Company entities.
/// </summary>
[ApiController]
public class CompanyController : ControllerBase
{
    private readonly IDataServices _dataServices;
    private readonly ILogger<CompanyController> _logger;

    public CompanyController(IDataServices dataServices, ILogger<CompanyController> logger)
    {
        _dataServices = dataServices;
        _logger = logger;
    }

    /// <summary>
    /// Gets the list of companies matching a specific page sorted by the title.
    /// </summary>
    /// <param name="start">The starting index of companies to retrieve.  Optional; 0 if not specified.</param>
    /// <param name="pageSize">The number of entries to retrieve.  Optional; 25 if not specified.</param>
    /// <returns>The companies starting from a given index and page size.</returns>
    [HttpGet("/api/company/list/{start:int?}/{pageSize:int?}", Name = nameof(GetAllCompanies))]
    public async Task<IEnumerable<Company>> GetAllCompanies(int start = 0, int pageSize = 25)
    {
        _logger.LogInformation($"Getting companies from {start} to {start + pageSize}...");
        var result = await _dataServices.Companies.GetList(start, pageSize);
        return result;
    }

    /// <summary>
    /// Adds a new company to the database.  Set the ID to the empty string ""
    /// and a new ID will be assigned automatically.  The returned entity will
    /// have the new ID.
    /// </summary>
    /// <param name="company">The company instance to add.</param>
    [HttpPost("/api/company/add", Name = nameof(AddCompany))]
    public async Task<Company> AddCompany([FromBody] Company company)
    {
        _logger.LogInformation("Adding a new company...");
        var result = await _dataServices.Companies.AddAsync(company);
        return result;
    }

    /// <summary>
    /// Deletes a Company given an ID.  Deletes all Employees that reference the
    /// Company as well.
    /// </summary>
    /// <param name="company">The company instance to add.</param>
    [HttpDelete("/api/company/delete/{id}", Name = nameof(DeleteCompany))]
    public async Task<DeleteResult> DeleteCompany(string id)
    {
        _logger.LogInformation($"Deleting company with ID {id}");
        var result = await _dataServices.Companies.DeleteAsync(id);
        await _dataServices.Employees.DeleteByCompany(id);
        return result;
    }

    /// <summary>
    /// Gets a Company by ID
    /// </summary>
    /// <param name="id">The ID of the company to retrieve.</param>
    /// <param name="full">When specified, returns the rich object</param>
    /// <returns>The Company instance that matches the ID.</returns>
    [HttpGet("/api/company/{id}/{full?}", Name = nameof(GetCompany))]
    public async Task<Company?> GetCompany(string id, bool full = false)
    {
        _logger.LogInformation($"Getting company with ID: {id} ({full})");

        var company = full
            ? await _dataServices.Companies.GetFullEntity(id)
            : await _dataServices.Companies.GetAsync(id);

        return company;
    }
}
