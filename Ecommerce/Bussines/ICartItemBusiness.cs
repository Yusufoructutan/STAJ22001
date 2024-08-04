using Ecommerce.Repository.Entity;

public interface ICartItemBusiness
{
    Task AddToCartAsync(CartItem cartItem);
    Task RemoveFromCartAsync(int cartItemId);
    Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync();
}
