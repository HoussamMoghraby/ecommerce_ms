using System;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
    public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery request, CancellationToken cancellationToken)
    {
        var orders = await dbContext.Orders
            .Where(o => o.CustomerId == CustomerId.Of(request.CustomerId))
            .ToListAsync(cancellationToken);

        var ordersDto = orders.ToOrderDtos();

        return new GetOrdersByCustomerResult(ordersDto);
    }
}
