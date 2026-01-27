namespace Catalog.API.Products.DeleteProduct;
//public record DeleteProductRequesst(Guid Id);
public record DeleteProductResponse(bool IsSuccess);
public class DeleteProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id}",
            async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));
                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
        .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("DeleteProduct")
        .WithSummary("Delete Product")
        .WithDescription("Udpate Product");
    }
}
