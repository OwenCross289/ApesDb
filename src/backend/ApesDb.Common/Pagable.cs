namespace ApesDb.Common;

public sealed class Pagable<T>
{
    public Pagable(IReadOnlyList<T> items, int total, int filteredTotal, int page, int pageSize)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentOutOfRangeException.ThrowIfNegative(total);
        ArgumentOutOfRangeException.ThrowIfNegative(filteredTotal);
        ArgumentOutOfRangeException.ThrowIfLessThan(page, 1);
        ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1);

        Items = items;
        Total = total;
        FilteredTotal = filteredTotal;
        Page = page;
        PageSize = pageSize;
    }

    public IReadOnlyList<T> Items { get; }

    public int Total { get; }

    public int FilteredTotal { get; }

    public int Page { get; }

    public int PageSize { get; }
}
