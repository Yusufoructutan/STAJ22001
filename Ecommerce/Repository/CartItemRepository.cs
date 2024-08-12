using Ecommerce.Repository.Entity;
using Ecommerce.Repository.Models;
using Microsoft.EntityFrameworkCore;

public class CartItemRepository : ICartItemRepository
{
    private readonly ECommerceContext _context;

    public CartItemRepository(ECommerceContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CartItem cartItem)
    {
        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
    }

    public async Task ClearCartAsync(int userId)
    {
        var cartItems = _context.CartItems.Where(ci => ci.UserId == userId);
        _context.CartItems.RemoveRange(cartItems);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int cartItemId)
    {
        var cartItem = await _context.CartItems.FindAsync(cartItemId);
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<CartItem>> GetCartItemsByUserIdAsync(int userId)
    {
        return await _context.CartItems
            .Where(ci => ci.UserId == userId)
            .ToListAsync();
    }

    public async Task UpdateAsync(CartItem cartItem)
    {
        var existingCartItem = await _context.CartItems.FindAsync(cartItem.CartItemId);
        if (existingCartItem != null)
        {
            existingCartItem.Quantity = cartItem.Quantity;
            _context.CartItems.Update(existingCartItem);
            await _context.SaveChangesAsync();
        }
    }




}
