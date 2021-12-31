namespace Api.Domain.Core;

/// <summary>
/// Abstract base class for all MongoDB entities.
/// </summary>
public abstract class MongoEntityBase : IMongoEntity
{
    private string _id = null!;
    private string _label = null!;

    public string Id
    {
        get { return this._id; }
        set { this._id = value; }
    }

    public string Label {
        get { return this._label; }
        set { this._label = value; }
    }
}