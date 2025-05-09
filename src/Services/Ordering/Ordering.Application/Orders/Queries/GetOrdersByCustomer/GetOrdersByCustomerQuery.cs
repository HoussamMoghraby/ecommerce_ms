using System;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerResult>
{
    
}
public record GetOrdersByCustomerResult(IEnumerable<OrderDto> Orders);

public class GetOrdersByCustomerValidator : AbstractValidator<GetOrdersByCustomerQuery>
{
    public GetOrdersByCustomerValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerName is required");
    }
}
