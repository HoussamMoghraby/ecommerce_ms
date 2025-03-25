
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
namespace BasketAPI.Data;


// Proxy pattern: CacheRepository class acts as a proxy by forwarding requests to BasetRepository
// Decorator pattern: CacheRepository class acts as decorator as it extends the BaseRepository implementations without atlering it.
public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
{
    public async Task<string> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        // delete basket from cache
        await cache.RemoveAsync(userName, cancellationToken);
        // delete basket from database
        return await repository.DeleteBasket(userName, cancellationToken);
    }
    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
    {
        var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

        // if cache isn't empty => return data from cache
        if (!string.IsNullOrEmpty(cachedBasket))
            return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
        // if cache is empty => get basket from database and update cache
        var basket = await repository.GetBasket(userName, cancellationToken);
        await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
    {
        // store basket in database
        ShoppingCart cart = await repository.StoreBasket(basket, cancellationToken);
        // store basket in cache
        await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(cart), cancellationToken);
        return cart;
    }
}
