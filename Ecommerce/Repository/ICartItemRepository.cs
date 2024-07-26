using Ecommerce.Repository.Entity;
using Ecommerce.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public interface ICartItemRepository
    {
        Task AddAsync(CartItem cartItem);
        Task DeleteAsync(int cartItemId);
        Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId);

        Task ClearCartAsync(int userId);

    }
}