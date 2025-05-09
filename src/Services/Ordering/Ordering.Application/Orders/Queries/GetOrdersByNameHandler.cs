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
        var orderDtos = MapToOrdersDto(orders);
        return new GetOrdersByNameResult(orderDtos);
    }

    private IEnumerable<OrderDto> MapToOrdersDto(IEnumerable<Order> orders)
    {
        return orders.Select(o => new OrderDto(

            Id: o.Id.Value,
            CustomerId: o.CustomerId.Value,
            OrderName: o.OrderName.Value,
            ShippingAddress: new AddressDto
            (
                FirstName: o.ShippingAddress.FirstName,
                LastName: o.ShippingAddress.LastName,
                EmailAddress: o.ShippingAddress.EmailAddress,
                AddressLine: o.ShippingAddress.AddressLine,
                Country: o.ShippingAddress.Country,
                State: o.ShippingAddress.State,
                ZipCode: o.ShippingAddress.ZipCode
            ),
            BillingAddress: new AddressDto
            (
                FirstName: o.BillingAddress.FirstName,
                LastName: o.BillingAddress.LastName,
                EmailAddress: o.BillingAddress.EmailAddress,
                AddressLine: o.BillingAddress.AddressLine,
                Country: o.BillingAddress.Country,
                State: o.BillingAddress.State,
                ZipCode: o.BillingAddress.ZipCode
            ),
            Payment: new PaymentDto
            (
                CardName: o.Payment.CardName,
                CardNumber: o.Payment.CardNumber,
                Expiration: o.Payment.Expiration,
                Cvv: o.Payment.CVV,
                PaymentMethod: o.Payment.PaymentMethod
            ),
            Status: o.Status,
            OrderItems: o.OrderItems.Select(oi => new OrderItemDto
            (
                OrderId: oi.OrderId.Value,
                ProductId: oi.ProductId.Value,
                Quantity: oi.Quantity,
                Price: oi.Price
            )).ToList()
        )).ToList();
    }
}
