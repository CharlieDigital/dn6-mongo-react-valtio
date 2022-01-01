namespace Api.Domain.Model;

/// <summary>
/// Models an employee.
/// </summary>
public class Employee : MongoEntityBase
{
    /// <summary>
    /// The first name of the employee.
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    /// The last name of the employee.
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    /// A reference to the Company that an Employee works for.
    /// </summary>
    [MongoRef(typeof(Company))]
    public EntityRef? Company { get; set; }
}