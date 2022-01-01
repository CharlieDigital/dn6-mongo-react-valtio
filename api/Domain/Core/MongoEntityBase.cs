namespace Api.Domain.Core;

/// <summary>
/// Abstract base class for all MongoDB entities.
/// </summary>
public abstract class MongoEntityBase : IMongoEntity
{
    /// <summary>
    /// The ID of the entity in the MongoDB
    /// </summary>
    public string Id { get; set; } = null!;

    /// <summary>
    /// The name associated with the entity in the MongoDB
    /// </summary>
    public string Label { get; set; } = null!;
}