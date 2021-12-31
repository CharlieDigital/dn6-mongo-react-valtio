

namespace Api.Domain.Model;

/// <summary>
/// Models a Company entity.
/// </summary>
public class Company
{
    [BsonId(IdGenerator = typeof(CombGuidGenerator))]
    public Guid Id;
    public string? Name;
    public string? Address;
    public string? WebUrl;
}
