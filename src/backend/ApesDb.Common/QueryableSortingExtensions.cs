using System.ComponentModel;
using System.Linq.Expressions;

namespace ApesDb.Common;

public static class QueryableSortingExtensions
{
    public static IOrderedQueryable<TSource> SortBy<TSource, TProperty>(
        this IQueryable<TSource> query,
        ListSortDirection direction,
        Expression<Func<TSource, TProperty>> property
    )
    {
        return direction switch
        {
            ListSortDirection.Ascending => query.OrderBy(property),
            ListSortDirection.Descending => query.OrderByDescending(property),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Unsupported sort direction."),
        };
    }

    public static IOrderedQueryable<TSource> SortBy<TSource, TProperty, TSecondaryProperty>(
        this IQueryable<TSource> query,
        ListSortDirection direction,
        Expression<Func<TSource, TProperty>> property,
        Expression<Func<TSource, TSecondaryProperty>> secondaryProperty
    )
    {
        return direction switch
        {
            ListSortDirection.Ascending => query.OrderBy(property).ThenBy(secondaryProperty),
            ListSortDirection.Descending => query.OrderByDescending(property).ThenByDescending(secondaryProperty),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Unsupported sort direction."),
        };
    }

    public static IOrderedQueryable<TSource> SortBy<TSource, TProperty, TSecondaryProperty, TTertiaryProperty>(
        this IQueryable<TSource> query,
        ListSortDirection direction,
        Expression<Func<TSource, TProperty>> property,
        Expression<Func<TSource, TSecondaryProperty>> secondaryProperty,
        Expression<Func<TSource, TTertiaryProperty>> tertiaryProperty
    )
    {
        return direction switch
        {
            ListSortDirection.Ascending => query.OrderBy(property).ThenBy(secondaryProperty).ThenBy(tertiaryProperty),
            ListSortDirection.Descending => query
                .OrderByDescending(property)
                .ThenByDescending(secondaryProperty)
                .ThenByDescending(tertiaryProperty),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, "Unsupported sort direction."),
        };
    }
}
