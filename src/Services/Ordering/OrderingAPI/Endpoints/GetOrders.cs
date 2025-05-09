namespace OrderingAPI.Endpoints;

public class GetOrders : ICarterModule
{
    public record GetOrdersResponse(PaginatedResult<OrderDto> Orders);
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/orders", async ([AsParameters] PaginationRequest paginationRequest, ISender sender) =>
        {
            var query = new GetOrdersQuery(paginationRequest);
            var result = await sender.Send(query);
            var response = result.Adapt<GetOrdersResponse>();
            return Results.Ok(result);
        })
        .WithName("GetOrders")
        .Produces<GetOrdersResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Orders")
        .WithDescription("Get Orders");

    }
}
