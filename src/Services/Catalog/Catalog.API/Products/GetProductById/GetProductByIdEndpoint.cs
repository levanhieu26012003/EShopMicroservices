
using Catalog.API.Products.GetProductById;

namespace Catalog.API.Products.GetProducts;

public record GetProductByIdResponse(Product Product);


public class GetProductByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id}", async (Guid id,ISender sender) =>
        {
            var result = await sender.Send(new GetProductByIdQuery(id));
            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        })
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("GetProductByID")
        .WithSummary("Get Product By ID")
        .WithDescription("Get Product By ID");
    }
}