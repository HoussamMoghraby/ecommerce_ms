using System;

namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

public record UpdateProductResponse(bool IsSuccess);
public class UpdateProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
        {
            UpdateProductCommand command = request.Adapt<UpdateProductCommand>();
            UpdateProductResult result = await sender.Send(command);
            UpdateProductResponse response = result.Adapt<UpdateProductResponse>();
            return Results.Created($"/products/{request.Id}", response);
        })
        .WithName("Update Product")
        .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Update Product")
        .WithDescription("Update Product");
    }
}
