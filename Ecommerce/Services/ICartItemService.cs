using Ecommerce.DTO;

public interface ICartItemService
{
    Task AddToCartAsync(CartItemCreateDto cartItemCreateDto);
    Task RemoveFromCartAsync(int cartItemId);
    Task<IEnumerable<CartItemDto>> GetCartItemsAsync();
    Task UpdateCartItemAsync(CartItemUpdateDto cartItemUpdateDto);
}
