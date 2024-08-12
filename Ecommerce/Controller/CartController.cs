using Ecommerce.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        await _cartService.AddToCartAsync(cartItemCreateDto);
        return Ok("Ürün sepete başarıyla eklendi.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItemUpdateDto cartItemUpdateDto)
    {
        if (cartItemUpdateDto == null || cartItemUpdateDto.CartItemId != id)
        {
            return BadRequest("Güncelleme verileri geçersiz.");
        }

        await _cartService.UpdateCartItemAsync(cartItemUpdateDto);
        return Ok("Ürün sepette başarıyla güncellendi.");
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
