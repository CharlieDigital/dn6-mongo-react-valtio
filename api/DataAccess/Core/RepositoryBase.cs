using System.Linq.Expressions;
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

    protected IMongoCollection<T> Collection
    {
        get { return _collection; }
    }

    protected MongoDbContext Context
    {
        get { return _context; }
    }

    /// <summary>
    /// Protected constructor which initializes the repository with the injected MongoDbContext.
    /// </summary>
    /// <param name="context">The MongoDbContext with the handle to the client.</param>
    protected RepositoryBase(MongoDbContext context)
    {
        _context = context;
        _collection = context.Database.GetCollection<T>(typeof(T).Name);
    }

    /// <summary>
    /// Adds an instance of a given entity into the database.  If the entity does not have an ID,
    /// a new one will be generated and assigned to the entity.
    /// </summary>
    /// <param name="entity">The entity to add to the database.</param>
    public async virtual Task<T> AddAsync(T entity)
    {
        if (string.IsNullOrEmpty(entity.Id))
        {
            // Assign an ID if the value isn't set.
            entity.Id = ObjectId.GenerateNewId().ToString();
        }

        await _collection.InsertOneAsync(entity);

        // Return the entity so that it receives the ID.
        return entity;
    }

    /// <summary>
    /// Deletes an instance from the database by the ID.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>The result of the deletion operation.</returns>
    public async virtual Task<DeleteResult> DeleteAsync(string id)
    {
        return await _collection.DeleteOneAsync<T>(e => e.Id == id);
    }

    /// <summary>
    /// Gets an instance of the entity by the ID.
    /// </summary>
    /// <param name="id">The ID of the entity to retrieve.</param>
    /// <returns>The instance of the entity that matches the ID.</returns>
    public async virtual Task<T> GetAsync(string id)
    {
        return (await _collection.FindAsync<T>(e => e.Id == id)).FirstOrDefault();
    }

    /// <summary>
    /// Gets a listing of the entities sorted on the title starting from the start index
    /// and retriving up to pageSize instances.
    /// </summary>
    /// <param name="start">The starting index of companies to retrieve.</param>
    /// <param name="pageSize">The number of entries to retrieve.</param>
    /// <param name="whereClause">An optional where clause to apply.</param>
    /// <returns>The specified number of entries starting from the specified start index sorted by title.</returns>
    public async virtual Task<IEnumerable<T>> GetList(int start, int pageSize, Expression<Func<T, bool>>? whereClause = null)
    {
        var query = _collection.AsQueryable();

        if (whereClause != null)
        {
            query = query.Where(whereClause);
        }

        return await query
            .OrderBy(e => e.Label)
            .Skip(start)
            .Take(pageSize)
            .ToListAsync();
    }
}