namespace Api.Domain.Model;

/// <summary>
/// Models an employee.
/// </summary>
public class Employee : MongoEntityBase
{
    /// <summary>
    /// The first name of the employee.
    /// </summary>
    public string? FirstName;

    /// <summary>
    /// The last name of the employee.
    /// </summary>
    public string? LastName;
}