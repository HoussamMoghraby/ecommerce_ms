using BasketAPI.Exceptions;
using BuildingBlocks.Exceptions;
using Marten;

namespace BasketAPI.Data;

public class BasketRepository(IDocumentSession session) : IBasketRepository
{
    public async Task<string> DeleteBasket(string userName, CancellationToken cancellationToken)
    {
        session.Delete<ShoppingCart>(userName);
        await session.SaveChangesAsync(cancellationToken);
        return userName;
    }

    public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken)
    {
        var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
        if (basket is null)
            throw new BasketNotFoundException(userName);
        return basket;
    }

    public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken)
    {
        session.Store(basket);
        await session.SaveChangesAsync(cancellationToken);
        return basket;
    }
}
