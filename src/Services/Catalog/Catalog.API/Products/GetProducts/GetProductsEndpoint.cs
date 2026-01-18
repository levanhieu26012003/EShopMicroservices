namespace Catalog.API.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

// Kế thừa Carter hỗ trợ minimal API
public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async (ISender sender) =>
        {
            // tạo Query để maping đến handle trong handler
            var result = await sender.Send(new GetProductsQuery());
            // map ping sang GetProductsResponse
            var response = result.Adapt<GetProductsResponse>();
            return Results.Ok(response);
        })
        .Produces<GetProductsResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithName("GetProducts")
        .WithSummary("Get Products")
        .WithDescription("Get Products"); // Tên hiện thị ở các nơi như Swagger
    }
}