
using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Basket.DeleteBasket;

public record DeleteBasketRequest(string UserName);
public record DeleteBasketResponse(string UserName);
public class DeleteBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
        {
            DeleteBasketRequest request = new DeleteBasketRequest(userName);
            DeleteBasketCommand command = request.Adapt<DeleteBasketCommand>();
            DeleteBasketResult result = await sender.Send(command);
            DeleteBasketResponse response = result.Adapt<DeleteBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteBasket")
        .Produces<DeleteBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Basket")
        .WithDescription("Delete Basket By User name");

        ;
    }
}
