using Ecommerce.Repository.Entity;
using Ecommerce.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public interface ICartItemRepository
    {
        // Sepete yeni bir ürün ekler.
        Task AddAsync(CartItem cartItem);

        // Sepetten belirtilen ID'ye sahip ürünü siler.
        Task DeleteAsync(int cartItemId);

        // Belirtilen kullanıcı ID'sine sahip kullanıcının sepetindeki tüm ürünleri getirir.
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId);

        // Belirtilen kullanıcı ID'sine sahip kullanıcının sepetini temizler.
        Task ClearCartAsync(int userId);

    }
}