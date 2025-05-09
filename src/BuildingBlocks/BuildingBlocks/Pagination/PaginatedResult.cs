using System;

namespace BuildingBlocks.Pagination;

public class PaginatedResult<TEntity>(int page, int pageSize, int totalCount, IEnumerable<TEntity> items)
    where TEntity : class
{
    public int Page { get; } = page;
    public int PageSize { get; } = pageSize;
    public int TotalCount { get; } = totalCount;
    public IEnumerable<TEntity> Items { get; } = items;
}
