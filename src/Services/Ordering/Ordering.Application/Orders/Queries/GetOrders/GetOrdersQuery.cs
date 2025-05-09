using System;
using BuildingBlocks.Pagination;

namespace Ordering.Application.Orders.Queries.GetOrders;

public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersResult>;
public record GetOrdersResult(PaginatedResult<OrderDto> Orders);

public class GetOrdersValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersValidator()
    {
        RuleFor(x => x.PaginationRequest).NotNull().WithMessage("PaginationRequest is required");
        RuleFor(x => x.PaginationRequest.PageIndex).GreaterThanOrEqualTo(0).WithMessage("PageIndex must be greater than or equal to 0");
        RuleFor(x => x.PaginationRequest.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than 0");
    }
}