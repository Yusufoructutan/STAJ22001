
using Ecommerce.DTO;
using Ecommerce.Repository.Entity;
using System.Security.Claims;

public class CartItemService : ICartItemService
{
    private readonly ICartItemBusiness _cartBusiness;
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemService(ICartItemRepository cartItemRepository, IHttpContextAccessor httpContextAccessor, ICartItemBusiness cartBusiness)
    {
        _cartItemRepository = cartItemRepository;
        _httpContextAccessor = httpContextAccessor;
        _cartBusiness = cartBusiness;
    }


    private int GetUserId()
    {
        // Token'dan kullanıcı ID'sini almak için uygun yöntemi kullanın
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userIdClaim);
    }


    public async Task AddToCartAsync(CartItemDto cartItemDto)
    {
        var userId = GetUserId();

        var cartItem = new CartItem
        {
            UserId = userId,
            ProductId = cartItemDto.ProductId,
            Quantity = cartItemDto.Quantity,
            CreatedDate = cartItemDto.CreatedDate
        };

        await _cartItemRepository.AddAsync(cartItem);
    }



    public async Task RemoveFromCartAsync(int cartItemId)
    {
        await _cartBusiness.RemoveFromCartAsync(cartItemId);
    }

    public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync()
    {
        var cartItems = await _cartBusiness.GetCartItemsByUserIdAsync();
        return cartItems.Select(ci => new CartItemDto
        {
            CartItemId = ci.CartItemId,
            UserId = ci.UserId,
            ProductId = ci.ProductId,
            Quantity = ci.Quantity
        });
    }
}
