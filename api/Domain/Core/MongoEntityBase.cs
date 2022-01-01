namespace Api.Domain.Core;

/// <summary>
/// Abstract base class for all MongoDB entities.
/// </summary>
public abstract class MongoEntityBase : IMongoEntity
{
    private string _id = null!;
    private string _label = null!;

    /// <summary>
    /// The ID of the entity in the MongoDB
    /// </summary>
    public string Id
    {
        get { return this._id; }
        set { this._id = value; }
    }

    /// <summary>
    /// The name associated with the entity in the MongoDB
    /// </summary>
    public string Label {
        get { return this._label; }
        set { this._label = value; }
    }
}