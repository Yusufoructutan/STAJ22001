using Ecommerce.DTO;

public interface ICartItemService
{
    Task AddToCartAsync(CartItemDto cartItemDto);
    Task RemoveFromCartAsync(int cartItemId);
    Task<IEnumerable<CartItemDto>> GetCartItemsAsync(int userId);
}
