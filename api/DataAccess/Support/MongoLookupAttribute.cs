namespace Api.DataAccess.Support;

/// <summary>
/// Attribute which is attached to a property that describes the lookup
/// to perform to load the entities that should be populated on the
/// property.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class MongoLookupAttribute : Attribute
{
    /// <summary>
    /// Default constructor.
    /// </summary>
    public MongoLookupAttribute() {

    }
}