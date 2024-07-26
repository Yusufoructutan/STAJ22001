using Ecommerce.Repository.Entity;
using Ecommerce.Repository;

namespace Ecommerce.Bussines
{
    public class CartItemBussiness:ICartItemBusiness
    {
        private readonly IRepository<CartItem> _cartItemRepository;

        public CartItemBussiness(IRepository<CartItem> cartItemRepository)
        {
            _cartItemRepository = cartItemRepository;
        }

        public async Task AddToCartAsync(CartItem cartItem)
        {
            await _cartItemRepository.AddAsync(cartItem);
        }

        public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId)
        {
            var cartItems = await _cartItemRepository.GetAllAsync();
            return cartItems.Where(ci => ci.UserId == userId).ToList();
        }
        public async Task RemoveFromCartAsync(int cartItemId)  
        {
            var cartItem = await _cartItemRepository.GetByIdAsync(cartItemId);
            if (cartItem != null)
            {
                await _cartItemRepository.DeleteAsync(Convert.ToInt32(cartItem));
            }
        }

    }
}
