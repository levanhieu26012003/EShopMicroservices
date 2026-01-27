namespace Basket.API.Repository;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation);
    Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellation);
    Task<bool> DeleteBasket(string userName, CancellationToken cancellation);
}
