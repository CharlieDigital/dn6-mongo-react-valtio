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
}