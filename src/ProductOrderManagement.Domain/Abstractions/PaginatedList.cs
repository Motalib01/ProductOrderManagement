using System.Collections.Generic;

namespace ProductOrderManagement.Application.Abstractions;

public class PaginatedList<T>
{
    public List<T> Items { get; }
    public int Page { get; }
    public int PageSize { get; }
    public int TotalCount { get; }

    public PaginatedList(List<T> items, int page, int pageSize, int totalCount)
    {
        Items = items;
        Page = page;
        PageSize = pageSize;
        TotalCount = totalCount;
    }

    public static PaginatedList<T> ToPaginatedList(IEnumerable<T> items, int page, int pageSize, int totalCount)
    {
        return new PaginatedList<T>(items.ToList(), page, pageSize, totalCount);
    }
}