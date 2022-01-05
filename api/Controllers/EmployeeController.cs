namespace Api.Controllers;

/// <summary>
/// API Contoller class for Employee entities.
/// </summary>
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IDataServices _dataServices;
    private readonly ILogger<EmployeeController> _logger;

    public EmployeeController(IDataServices dataServices, ILogger<EmployeeController> logger)
    {
        _dataServices = dataServices;
        _logger = logger;
    }

    /// <summary>
    /// Adds a new Employee to the database.  Set the ID to the empty string ""
    /// and a new ID will be assigned automatically.  The returned entity will
    /// have the new ID.
    /// </summary>
    /// <param name="employee">The Employee instance to add.</param>
    [HttpPost("/api/employee/add", Name = nameof(AddEmployee))]
    public async Task<Employee> AddEmployee([FromBody] Employee employee)
    {
        _logger.LogInformation("Adding a new employee...");
        var result = await _dataServices.Employees.AddAsync(employee);
        return result;
    }

    /// <summary>
    /// Deletes a Employee given an ID.
    /// </summary>
    /// <param name="employee">The Employee instance to add.</param>
    [HttpDelete("/api/employee/delete/{id}", Name = nameof(DeleteEmployee))]
    public async Task<DeleteResult> DeleteEmployee(string id)
    {
        _logger.LogInformation($"Deleting employee with ID {id}");
        var result = await _dataServices.Employees.DeleteAsync(id);
        return result;
    }

    /// <summary>
    /// Gets a Employee by ID
    /// </summary>
    /// <param name="id">The ID of the Employee to retrieve.</param>
    /// <returns>The Employee instance that matches the ID.</returns>
    [HttpGet("/api/employee/{id}", Name = nameof(GetEmployee))]
    public async Task<Employee> GetEmployee(string id)
    {
        _logger.LogInformation($"Getting employee with ID: {id}");
        var employee = await _dataServices.Employees.GetAsync(id);
        return employee;
    }

    /// <summary>
    /// Gets the list of Employees by the company ID.
    /// </summary>
    /// <param name="companyId">The ID of the company to retrieve employees for.</param>
    /// <param name="start">The starting index of companies to retrieve.</param>
    /// <param name="pageSize">The number of companies to retrieve.</param>
    /// <returns>The specified number of companies starting from the specified start index sorted by title.</returns>
    [HttpGet("/api/employee/company/{companyId}/{start:int?}/{pageSize:int?}", Name = nameof(GetByCompany))]
    public async Task<IEnumerable<Employee>> GetByCompany(string companyId, int start = 0, int pageSize = 25)
    {
        // Use something like this to prevent over-fetching:
        // https://docs.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-6.0&tabs=visual-studio-code#prevent-over-posting
        _logger.LogInformation($"Getting employee for company ID: {companyId}");
        var employees = await _dataServices.Employees.GetByCompany(companyId, start, pageSize);
        return employees;
    }
}