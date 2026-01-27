namespace Catalog.API.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);

public record GetProductsResponse(IEnumerable<Product> Products);

// Kế thừa Carter hỗ trợ minimal API
public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetProductsQuery>();
            var result = await sender.Send(query);
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