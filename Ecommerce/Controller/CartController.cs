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
    public async Task<IActionResult> AddToCart([FromBody] CartItemDto cartItemDto)
    {
        if (cartItemDto == null)
        {
            return BadRequest("Sepet ürünü verileri geçersiz.");
        }

        await _cartService.AddToCartAsync(cartItemDto);
        return Ok("Ürün sepete başarıyla eklendi.");
    }





    [HttpDelete("{id}")]
    public async Task<IActionResult> RemoveFromCart(int id)
    {
        await _cartService.RemoveFromCartAsync(id);
        return Ok("Ürün sepetten başarıyla kaldırıldı.");
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
