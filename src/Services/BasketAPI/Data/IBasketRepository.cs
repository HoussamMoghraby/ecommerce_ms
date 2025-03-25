namespace BasketAPI.Data;

public interface IBasketRepository
{
    Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken);
    Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken);
    Task<string> DeleteBasket(string userName, CancellationToken cancellationToken);

}
