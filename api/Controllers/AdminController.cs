namespace Api.Controllers;

/// <summary>
/// API Controller for administration functions.
/// </summary>
[ApiController]
[Authorize]
public class AdminController : ControllerBase
{
    private readonly IDataServices _dataServices;
    private readonly ILogger<AdminController> _logger;

    public AdminController(IDataServices dataServices, ILogger<AdminController> logger)
    {
        _dataServices = dataServices;
        _logger = logger;
    }

    /// <summary>
    /// Resets the environment by dropping the collections.
    /// </summary>
    [HttpDelete("/api/admin/reset", Name = nameof(ResetEnv))]
    public async Task ResetEnv()
    {
        await _dataServices.Admin.DropCollectionsAsync();
    }
}