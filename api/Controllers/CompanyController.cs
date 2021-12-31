
namespace Api.Controllers;

/// <summary>
/// API Contoller class for Company entities.
/// </summary>
[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly IDataServices _dataServices;
    private readonly ILogger<CompanyController> _logger;

    public CompanyController(IDataServices dataServices, ILogger<CompanyController> logger)
    {
        this._dataServices = dataServices;
        this._logger = logger;
    }

    /// <summary>
    /// Adds a new company to the database.
    /// </summary>
    /// <param name="company">The company instance to add.</param>
    [Route("/api/company/add")]
    [HttpPost]
    public async Task<IActionResult> AddCompany([FromBody] Company company)
    {
        _logger.LogInformation("Adding a new company...");
        Company result = await this._dataServices.Companies.AddAsync(company);
        return new OkObjectResult(result);
    }

    /// <summary>
    /// Deletes a company given an ID.
    /// </summary>
    /// <param name="company">The company instance to add.</param>
    [Route("/api/company/delete/{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteCompany(string id)
    {
        _logger.LogInformation($"Deleting company with ID {id}");
        DeleteResult result = await this._dataServices.Companies.DeleteAsync(id);
        return new OkObjectResult(result);
    }

    [Route("/api/company/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetCompany(string id) {
        _logger.LogInformation($"Getting company with ID: {id}");
        Company company = await this._dataServices.Companies.GetAsync(id);
        return new OkObjectResult(company);
    }
}
