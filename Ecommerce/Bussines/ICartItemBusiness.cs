using Ecommerce.Repository.Entity;

namespace Ecommerce.Bussines
{
    public interface ICartItemBusiness
    {

        Task AddToCartAsync(CartItem cartItem);
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId);

        Task RemoveFromCartAsync(int cartItemId);  // Yeni metod


    }
}
