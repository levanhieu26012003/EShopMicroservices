


namespace Basket.API.Repository
{
    public class BasketRepository(IDocumentSession session) : IBasketRepository
    {
        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation)
        {
            var basket = await session.LoadAsync<ShoppingCart>(userName, cancellation); // loadAsync: get by id, hiệu quả tối đa
                                                                                        // Query : dùng trong search, filter
            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellation)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellation);
            return basket;
        }
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellation)
        {
            session.Delete<ShoppingCart>(userName);
            await session.SaveChangesAsync(cancellation);
            return true;
        }

    }
}
