using Ecommerce.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    private readonly ICartItemService _cartService;

    public CartController(ICartItemService cartService)
    {
        _cartService = cartService;
    }
    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] CartItemCreateDto cartItemCreateDto)
    {
        if (cartItemCreateDto == null)
        {
            return BadRequest("Sepet ürünü verileri geçersiz.");
        }

        try
        {
            await _cartService.AddToCartAsync(cartItemCreateDto);
            return CreatedAtAction(nameof(AddToCart), new { productId = cartItemCreateDto.ProductId });
        }
        catch (ArgumentException ex)
        {
            // Geçersiz ürün ID'si veya diğer argüman hataları
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // Diğer genel hatalar
            // Loglama işlemi burada yapılabilir
            return StatusCode(500, "Sepete ürün eklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItemUpdateDto cartItemUpdateDto)
    {
        if (cartItemUpdateDto == null || cartItemUpdateDto.CartItemId != id)
        {
            return BadRequest("Güncelleme verileri geçersiz.");
        }

        await _cartService.UpdateCartItemAsync(cartItemUpdateDto);
        return Ok("urun sepette başarıyla güncellendi.");
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        await _cartService.RemoveFromCartAsync(id);
        return Ok("urun sepetten başarıyla kaldırıldı.");
    }

    [HttpGet]
    public async Task<IActionResult> GetCartItems()
    {
        try
        {
            var cartItems = await _cartService.GetCartItemsAsync();
            return Ok(cartItems);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
