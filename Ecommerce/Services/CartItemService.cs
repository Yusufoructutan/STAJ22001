using Ecommerce.Bussines;
using Ecommerce.DTO;
using Ecommerce.Repository.Entity;
using Ecommerce.Repository.Models;
using System.Threading.Tasks;
using Ecommerce.Services;

namespace Ecommerce.Services
{
   
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemBusiness _cartBusiness;

        public CartItemService(ICartItemBusiness cartBusiness)
        {
            _cartBusiness = cartBusiness;
        }

        public async Task AddToCartAsync(CartItemDto cartItemDto)
        {
            var cartItem = new CartItem
            {
                UserId = cartItemDto.UserId,
                ProductId = cartItemDto.ProductId,
                Quantity = cartItemDto.Quantity
            };
            await _cartBusiness.AddToCartAsync(cartItem);
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            await _cartBusiness.RemoveFromCartAsync(cartItemId);
        }

        public async Task<IEnumerable<CartItemDto>> GetCartItemsAsync(int userId)
        {
            var cartItems = await _cartBusiness.GetCartItemsByUserIdAsync(userId);
            return cartItems.Select(ci => new CartItemDto
            {
                CartItemId = ci.CartItemId,
                UserId = ci.UserId,
                ProductId = ci.ProductId,
                Quantity = ci.Quantity
            });
        }
       
    }
}
