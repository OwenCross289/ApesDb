using System.Linq.Expressions;

namespace ApesDb.Common;

public static class QueryableFilteringExtensions
{
    public static IQueryable<TSource> WhereEqual<TSource, TProperty>(
        this IQueryable<TSource> query,
        TProperty? value,
        Expression<Func<TSource, TProperty>> property
    )
        where TProperty : class
    {
        if (value is null)
        {
            return query;
        }

        var body = Expression.Equal(property.Body, Expression.Constant(value, typeof(TProperty)));
        return query.Where(Expression.Lambda<Func<TSource, bool>>(body, property.Parameters));
    }

    public static IQueryable<TSource> WhereEqual<TSource, TProperty>(
        this IQueryable<TSource> query,
        TProperty? value,
        Expression<Func<TSource, TProperty>> property
    )
        where TProperty : struct
    {
        if (value is null)
        {
            return query;
        }

        var body = Expression.Equal(property.Body, Expression.Constant(value.Value, typeof(TProperty)));
        return query.Where(Expression.Lambda<Func<TSource, bool>>(body, property.Parameters));
    }

    public static IQueryable<TSource> WhereEqual<TSource, TProperty>(
        this IQueryable<TSource> query,
        TProperty? value,
        Expression<Func<TSource, TProperty?>> property
    )
        where TProperty : struct
    {
        if (value is null)
        {
            return query;
        }

        var expectedValue = Expression.Convert(Expression.Constant(value.Value, typeof(TProperty)), typeof(TProperty?));
        var body = Expression.Equal(property.Body, expectedValue);
        return query.Where(Expression.Lambda<Func<TSource, bool>>(body, property.Parameters));
    }

    public static IQueryable<TSource> WhereContains<TSource, TProperty>(
        this IQueryable<TSource> query,
        IEnumerable<TProperty>? values,
        Expression<Func<TSource, TProperty>> property
    )
    {
        var materializedValues = values?.ToArray();
        if (materializedValues is not { Length: > 0 })
        {
            return query;
        }

        var body = Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.Contains),
            [typeof(TProperty)],
            Expression.Constant(materializedValues),
            property.Body
        );
        return query.Where(Expression.Lambda<Func<TSource, bool>>(body, property.Parameters));
    }

    public static IQueryable<TSource> WhereContains<TSource, TProperty>(
        this IQueryable<TSource> query,
        IEnumerable<TProperty>? values,
        Expression<Func<TSource, TProperty?>> property
    )
        where TProperty : struct
    {
        var materializedValues = values?.ToArray();
        if (materializedValues is not { Length: > 0 })
        {
            return query;
        }

        var hasValue = Expression.Property(property.Body, nameof(Nullable<TProperty>.HasValue));
        var selectedValue = Expression.Property(property.Body, nameof(Nullable<TProperty>.Value));
        var contains = Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.Contains),
            [typeof(TProperty)],
            Expression.Constant(materializedValues),
            selectedValue
        );
        var body = Expression.AndAlso(hasValue, contains);
        return query.Where(Expression.Lambda<Func<TSource, bool>>(body, property.Parameters));
    }

    public static IQueryable<TSource> WhereContains<TSource, TProperty>(
        this IQueryable<TSource> query,
        IEnumerable<TProperty>? values,
        Expression<Func<TSource, IQueryable<TProperty>>> properties
    )
    {
        var materializedValues = values?.ToArray();
        if (materializedValues is not { Length: > 0 })
        {
            return query;
        }

        var property = Expression.Parameter(typeof(TProperty), "property");
        var contains = Expression.Call(
            typeof(Enumerable),
            nameof(Enumerable.Contains),
            [typeof(TProperty)],
            Expression.Constant(materializedValues),
            property
        );
        var predicate = Expression.Lambda<Func<TProperty, bool>>(contains, property);
        var body = Expression.Call(
            typeof(Queryable),
            nameof(Queryable.Any),
            [typeof(TProperty)],
            properties.Body,
            Expression.Quote(predicate)
        );
        return query.Where(Expression.Lambda<Func<TSource, bool>>(body, properties.Parameters));
    }

    public static IQueryable<TSource> WhereContains<TSource>(
        this IQueryable<TSource> query,
        string? value,
        Expression<Func<TSource, string>> property
    )
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return query;
        }

        var normalizedValue = value.Trim().ToLowerInvariant();
        var loweredProperty = Expression.Call(property.Body, nameof(string.ToLower), Type.EmptyTypes);
        var body = Expression.Call(
            loweredProperty,
            nameof(string.Contains),
            Type.EmptyTypes,
            Expression.Constant(normalizedValue)
        );
        return query.Where(Expression.Lambda<Func<TSource, bool>>(body, property.Parameters));
    }

    public static IQueryable<TSource> WhereContains<TSource>(
        this IQueryable<TSource> query,
        string? value,
        Expression<Func<TSource, IQueryable<string>>> properties
    )
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return query;
        }

        var normalizedValue = value.Trim().ToLowerInvariant();
        var property = Expression.Parameter(typeof(string), "property");
        var loweredProperty = Expression.Call(property, nameof(string.ToLower), Type.EmptyTypes);
        var contains = Expression.Call(
            loweredProperty,
            nameof(string.Contains),
            Type.EmptyTypes,
            Expression.Constant(normalizedValue)
        );
        var predicate = Expression.Lambda<Func<string, bool>>(contains, property);
        var body = Expression.Call(
            typeof(Queryable),
            nameof(Queryable.Any),
            [typeof(string)],
            properties.Body,
            Expression.Quote(predicate)
        );
        return query.Where(Expression.Lambda<Func<TSource, bool>>(body, properties.Parameters));
    }

    public static IQueryable<TSource> WherePredicate<TSource, TValue>(
        this IQueryable<TSource> query,
        TValue? value,
        Func<TValue, Expression<Func<TSource, bool>>> predicateFactory
    )
        where TValue : class
    {
        if (value is null)
        {
            return query;
        }

        return query.Where(predicateFactory(value));
    }

    public static IQueryable<TSource> WherePredicate<TSource, TValue>(
        this IQueryable<TSource> query,
        TValue? value,
        Func<TValue, Expression<Func<TSource, bool>>> predicateFactory
    )
        where TValue : struct
    {
        if (value is null)
        {
            return query;
        }

        return query.Where(predicateFactory(value.Value));
    }
}
