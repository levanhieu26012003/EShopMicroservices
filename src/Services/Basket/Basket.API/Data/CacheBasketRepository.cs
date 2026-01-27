
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data;

public class CacheBasketRepository
    (IBasketRepository repository, IDistributedCache cache)
    : IBasketRepository
{

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellation)
    {
        var basketCache = await cache.GetStringAsync(userName, cancellation); // truyền key và nhận data
        if (!string.IsNullOrEmpty(basketCache)) 
            return JsonSerializer.Deserialize<ShoppingCart>(basketCache); // trả về data


        var basket = await repository.GetBasket(userName, cancellation); 
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellation);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellation)
    {
        await cache.SetStringAsync( basket.UserName, JsonSerializer.Serialize(basket), cancellation);
        return await repository.StoreBasket(basket, cancellation);
    }
    public async Task<bool> DeleteBasket(string userName, CancellationToken cancellation)
    {
        await cache.RemoveAsync(userName, cancellation);
        return await repository.DeleteBasket(userName,cancellation);
    }
}
