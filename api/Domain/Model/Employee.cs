namespace Api.Domain.Model;

/// <summary>
/// Core data model for an employee.
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
    /// The salary assigned to the employee.
    /// </summary>
    public int Salary { get; set; } = 0;

    /// <summary>
    /// A reference to the Company that an Employee works for.
    /// </summary>
    [MongoRef(typeof(Company))]
    public EntityRef? Company { get; set; }
}