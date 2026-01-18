namespace Catalog.API.Products.UpdateProduct;
public record UpdateProductRequesst(Guid Id, string Name, List<string> Category, string Decription, string ImageFile, decimal Price);
public record UpdateProductResponse(bool IsSuccess); // phải đặt property viết hoa, viết thường ko map được
public class UpdateProductEndPoint : ICarterModule // lưu ý access modifier
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("products",
            async (UpdateProductRequesst request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
        .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("UpdateProduct")
        .WithSummary("Update Product")
        .WithDescription("Udpate Product");
    }
}
