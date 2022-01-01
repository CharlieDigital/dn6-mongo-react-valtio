namespace Api.DataAccess.Core;

/// <summary>
/// Abstract base class which provides core implementation of operations for
/// accessing collections of specific types of objects.
/// </summary>
/// <typeparam name="T">The type of the entity being saved.</typeparam>
public abstract class RepositoryBase<T> where T: IMongoEntity
{
    private readonly MongoDbContext _context;

    private readonly IMongoCollection<T> _collection;

    /// <summary>
    /// Protected constructor which initializes the repository with the injected MongoDbContext.
    /// </summary>
    /// <param name="context">The MongoDbContext with the handle to the client.</param>
    protected RepositoryBase(MongoDbContext context)
    {
        this._context = context;
        this._collection = context.Database.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
    }

    /// <summary>
    /// Adds an instance of a given entity into the database.  If the entity does not have an ID,
    /// a new one will be generated and assigned to the entity.
    /// </summary>
    /// <param name="entity">The entity to add to the database.</param>
    public async Task<T> AddAsync(T entity)
    {
        if (string.IsNullOrEmpty(entity.Id))
        {
            // Assign an ID if the value isn't set.
            entity.Id = ObjectId.GenerateNewId().ToString();
        }

        await this._collection.InsertOneAsync(entity);

        // Return the entity so that it receives the ID.
        return entity;
    }

    /// <summary>
    /// Deletes an instance from the database by the ID.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>The result of the deletion operation.</returns>
    public async Task<DeleteResult> DeleteAsync(string id)
    {
        return await this._collection.DeleteOneAsync<T>(e => e.Id == id);
    }

    /// <summary>
    /// Gets an instance of the entity by the ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>The instance of the entity that matches the ID.</returns>
    public async Task<T> GetAsync(string id)
    {
        return (await this._collection.FindAsync<T>(e => e.Id == id)).FirstOrDefault();
    }
}