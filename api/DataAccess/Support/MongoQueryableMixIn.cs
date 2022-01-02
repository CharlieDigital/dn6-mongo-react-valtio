namespace Api.DataAccess.Support;

// See: https://stackoverflow.com/a/49227402/116051

public static class MongoQueryableMixIn
{
    public static Task<List<T>> ToMongoListAsync<T>(this IQueryable<T> mongoQuery)
    {
        return ((IMongoQueryable<T>) mongoQuery).ToListAsync();
    }
}