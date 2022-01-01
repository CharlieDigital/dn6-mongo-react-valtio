namespace Api.Domain.Core;

/// <summary>
/// Represents an entity reference from one entity to another.  It inherits
/// the base properties of ID and Label from the base entities which allows
/// for efficient display of the relationship in the UI.
/// </summary>
public class EntityRef : MongoEntityBase
{
    /// <summary>
    /// The collection that the entity exists in.
    /// </summary>
    public string Collection { get; set; } = null!;
}