namespace Ordering.Application.Orders.Queries;

public class GetOrderByNameHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByNameQuery, GetOrdersByNameResult>
{
    public async Task<GetOrdersByNameResult> Handle(GetOrdersByNameQuery query, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders.Where(o => o.OrderName == OrderName.Of(query.Name)).ToListAsync(cancellationToken);
        if (orders == null || !orders.Any())
        {
            throw new NotFoundException($"No orders found for name {query.Name}");
        }
        var orderDtos = orders.ToOrderDtos();
        return new GetOrdersByNameResult(orderDtos);
    }

}
