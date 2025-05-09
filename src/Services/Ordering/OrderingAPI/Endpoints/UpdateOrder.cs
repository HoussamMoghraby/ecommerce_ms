using System;
using Ordering.Application.Orders.Commands.UpdateOrder;

namespace OrderingAPI.Endpoints;

public record UpdateOrderRequest(OrderDto Order);
public record UpdateOrderResponse(bool IsSuccess);
public class UpdateOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/orders/{id}", async (Guid id, UpdateOrderRequest request, ISender sender) =>
        {

            var command = request.Adapt<UpdateOrderCommand>() with { Order = request.Order with { Id = id } };
            UpdateOrderResult result = await sender.Send(command);
            return Results.Ok(result.Adapt<UpdateOrderResponse>());
        })
        .WithName("UpdateOrder")
        .Produces<UpdateOrderResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Update Order")
        .WithDescription("Update Order");
    }
};
