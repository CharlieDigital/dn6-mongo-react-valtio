namespace Api.Domain.Core;

/// <summary>
/// Interface for MongoDB entities.
/// </summary>
public interface IMongoEntity
{
    /// <summary>
    /// The ID of the entity in the MongoDB
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    string Id { get; set; }

    /// <summary>
    /// The name associated with the entity in the MongoDB
    /// </summary>
    string Label { get; set; }
}