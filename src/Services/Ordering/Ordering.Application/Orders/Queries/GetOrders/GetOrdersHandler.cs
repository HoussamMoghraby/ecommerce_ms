using System;
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
{
    public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Skip(request.PaginationRequest.PageIndex * request.PaginationRequest.PageSize)
            .Take(request.PaginationRequest.PageSize)
            .ToListAsync(cancellationToken);
        var ordersDto = orders.ToOrderDtos();
        var totalCount = await dbContext.Orders.CountAsync(cancellationToken);
        var paginatedResult = new PaginatedResult<OrderDto>(request.PaginationRequest.PageIndex, request.PaginationRequest.PageSize, totalCount, ordersDto);
        return new GetOrdersResult(paginatedResult);

    }
}
