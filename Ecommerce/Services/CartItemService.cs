
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


    public async Task AddToCartAsync(CartItemCreateDto cartItemCreateDto)
    {
        var userId = GetUserId();

        var cartItem = new CartItem
        {
            UserId = 1,
            ProductId = cartItemCreateDto.ProductId,
            Quantity = cartItemCreateDto.Quantity,
            CreatedDate = DateTime.Now
        };

        await _cartItemRepository.AddAsync(cartItem);
    }

    public async Task UpdateCartItemAsync(CartItemUpdateDto cartItemUpdateDto)
    {
        // Kullanıcının sepetindeki ilgili ürünün mevcut durumunu al
        var existingCartItems = await _cartBusiness.GetCartItemsByUserIdAsync();
        var existingCartItem = existingCartItems.FirstOrDefault(ci => ci.CartItemId == cartItemUpdateDto.CartItemId);

        if (existingCartItem == null)
        {
            throw new InvalidOperationException("Güncellenecek ürün bulunamadı.");
        }

        // Sadece quantity değerini güncelle
        existingCartItem.Quantity = cartItemUpdateDto.Quantity;

        // Güncellenmiş ürün nesnesini repoya gönder
        await _cartBusiness.UpdateCartItemAsync(existingCartItem);
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
            UserId =1,
            ProductId = ci.ProductId,
            Quantity = ci.Quantity
        });
    }
}
