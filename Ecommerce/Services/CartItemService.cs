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
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
        {
            throw new InvalidOperationException("Kullanıcı ID'si alınamadı.");
        }
        return userId;
    }

    public async Task AddToCartAsync(CartItemCreateDto cartItemCreateDto)
    {
        var userId = GetUserId();

        if (cartItemCreateDto.ProductId <= 0)
        {
            throw new ArgumentException("Geçersiz ürün ID'si.");
        }

        var cartItem = new CartItem
        {
            UserId = userId,
            ProductId = cartItemCreateDto.ProductId,
            Quantity = cartItemCreateDto.Quantity,
            CreatedDate = DateTime.UtcNow // UTC zamanı kullanılması daha uygun olabilir
        };

        try
        {
            await _cartItemRepository.AddAsync(cartItem);
        }
        catch (Exception ex)
        {
            // Hata loglama ve yönetimi
            throw new InvalidOperationException("Sepete ürün eklenirken bir hata oluştu.", ex);
        }
    }

    public async Task UpdateCartItemAsync(CartItemUpdateDto cartItemUpdateDto)
    {
        var userId = GetUserId();
        var existingCartItems = await _cartBusiness.GetCartItemsByUserIdAsync();
        var existingCartItem = existingCartItems.FirstOrDefault(ci => ci.CartItemId == cartItemUpdateDto.CartItemId);

        if (existingCartItem == null)
        {
            throw new InvalidOperationException("Güncellenecek urun bulunamadı.");
        }

        existingCartItem.Quantity = cartItemUpdateDto.Quantity;

        try
        {
            await _cartBusiness.UpdateCartItemAsync(existingCartItem);
        }
        catch (Exception ex)
        {
            // Hata loglama ve yönetimi
            throw new InvalidOperationException("Sepet urun güncellenirken bir hata oluştu.", ex);
        }
    }

    public async Task RemoveFromCartAsync(int cartItemId)
    {
        try
        {
            await _cartBusiness.RemoveFromCartAsync(cartItemId);
        }
        catch (Exception ex)
        {
            // Hata loglama ve yönetimi
            throw new InvalidOperationException("Sepet urunü silinirken bir hata oluştu.", ex);
        }
    }

    public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync()
    {
        var cartItems = await _cartBusiness.GetCartItemsByUserIdAsync();
        return cartItems.Select(ci => new CartItemDto
        {
            CartItemId = ci.CartItemId,
            ProductId = ci.ProductId,
            Quantity = ci.Quantity
        });
    }
}
