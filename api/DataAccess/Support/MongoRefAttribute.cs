namespace Api.DataAccess.Support;

/// <summary>
/// This attribute is used on an EntityRef to point to the parent type.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class MongoRefAttribute: Attribute
{
    private readonly Type _type;

    /// <summary>
    /// Specify the type that the entity references.
    /// </summary>
    /// <param name="type">The type that the entity references.</param>
    public MongoRefAttribute(Type type)
    {
        this._type = type;
    }
}