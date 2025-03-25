using System;
using Microsoft.AspNetCore.Mvc;

namespace BasketAPI.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);
public class StoreBasketEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            StoreBasketCommand command = new StoreBasketCommand(request.Cart);
            StoreBasketResult result = await sender.Send(command);
            StoreBasketResponse response = result.Adapt<StoreBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("StoreBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Store Basket")
        .WithDescription("Add/Update Basket");
    }
}
