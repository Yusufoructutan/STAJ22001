using Ecommerce.Repository.Entity;

public interface ICartItemRepository
{
    Task AddAsync(CartItem cartItem);
    Task DeleteAsync(int cartItemId);
    Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId);
    Task ClearCartAsync(int userId); // Bu satırı ekleyin

    Task UpdateAsync(CartItem cartItem);
}
