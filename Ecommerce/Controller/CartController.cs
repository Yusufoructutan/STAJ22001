using Ecommerce.DTO;
using Ecommerce.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecommerce.Controller
{
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

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartItems(int userId)
        {
            var cartItems = await _cartService.GetCartItemsAsync(userId);
            return Ok(cartItems);
        }
    }
}
