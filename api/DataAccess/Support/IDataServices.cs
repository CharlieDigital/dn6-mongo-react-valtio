namespace Api.DataAccess.Support;

/// <summary>
/// This interface is used for the DI container and defines the set of repositories.
/// Register each repository here to make it available to controllers without having
/// to manually add each type to the controllers.
/// </summary>
public interface IDataServices
{
    /// <summary>
    /// Repository for accessing Company instances.
    /// </summary>
    public CompanyRepository Companies { get; }

    /// <summary>
    /// Repository for accessing Employee instances.
    /// </summary>
    public EmployeeRepository Employees { get; }
}