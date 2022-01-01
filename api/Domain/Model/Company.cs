namespace Api.Domain.Model;

/// <summary>
/// Models a Company entity.
/// </summary>
public class Company : MongoEntityBase
{
    /// <summary>
    /// The address of the company.
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// The URL of the website for the given company.
    /// </summary>
    public string? WebUrl { get; set; }

    /// <summary>
    /// The list of Employees associated with this Company.
    /// </summary>
    /// <remarks>
    /// To efficiently store this, we want to map the Company ID on the Employee instead
    /// of mapping the Employee ID on the Company.  This is because a Company will have
    /// many Employees while an Employee will have typically 1 but perhaps more Companies
    /// so it is generally more efficient to "lookup all Employees for a given Company ID"
    /// than to "lookup all Employee IDs for a given Company".  So we want to ignore this
    /// on save.
    /// </remarks>
    [BsonIgnore]
    [MongoLookup]
    public IEnumerable<Employee> Employees { get; set; } = new List<Employee>();
}