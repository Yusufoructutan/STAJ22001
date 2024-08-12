using Ecommerce.Repository.Entity;
using Ecommerce.Repository;
using System.Security.Claims;

public class CartItemBusiness : ICartItemBusiness
{
    private readonly IRepository<CartItem> _cartItemRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CartItemBusiness(IRepository<CartItem> cartItemRepository, IHttpContextAccessor httpContextAccessor)
    {
        _cartItemRepository = cartItemRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task AddToCartAsync(CartItem cartItem)
    {
        await _cartItemRepository.AddAsync(cartItem);
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync()
    {
        var userId = GetUserIdFromToken();
        if (userId == null)
        {
            throw new UnauthorizedAccessException("Kullanıcı kimliği alınamadı.");
        }

        var cartItems = await _cartItemRepository.GetAllAsync();
        return cartItems.Where(ci => ci.UserId == userId.Value).ToList();
    }
    public async Task UpdateCartItemAsync(CartItem cartItem)
    {
        await _cartItemRepository.UpdateAsync(cartItem);
    }

    public async Task<CartItem> GetCartItemByIdAsync(int cartItemId)
    {
        var cartItems = await GetCartItemsByUserIdAsync();
        return cartItems.FirstOrDefault(ci => ci.CartItemId == cartItemId);
    }

    public async Task RemoveFromCartAsync(int cartItemId)
    {
        var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
        if (cartItem != null)
        {
            await _cartItemRepository.DeleteAsync(cartItemId);
        }
    }

    private int? GetUserIdFromToken()
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

        if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
        {
            return userId;
        }

        return null;
    }
}
