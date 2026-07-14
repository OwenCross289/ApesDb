namespace ApesDb.Common;

public static class QueryablePagingExtensions
{
    public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> query, int page, int pageSize)
    {
        ArgumentNullException.ThrowIfNull(query);
        ArgumentOutOfRangeException.ThrowIfLessThan(page, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

        var skip = (page - 1) * pageSize;
        return query.Skip(skip).Take(pageSize);
    }
}
