
namespace Basket.API.Basket.Storebasket;
public record StoreBasketResponse(string UserName);
public record StoreBasketRequest(ShoppingCart Cart): ICommand<StoreBasketResponse>;
public class StorebasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sennder) =>
        {
            var command = request.Adapt<StoreBasketCommand>();
            var result =  await sennder.Send(command);
            var response = result.Adapt<StoreBasketResponse>();
            return Results.Created($"/basket/{response.UserName}",response);
        })
              .WithName("CreateBasket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Create Basket")
        .WithDescription("Create Basket"); ;
    }
}
