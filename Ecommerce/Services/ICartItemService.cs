using Ecommerce.DTO;

public interface ICartItemService
{
    Task AddToCartAsync(CartItemDto cartItem);
    Task RemoveFromCartAsync(int cartItemId);
    Task<IEnumerable<CartItemDto>> GetCartItemsAsync();
}
