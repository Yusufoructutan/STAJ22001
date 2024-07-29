using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ecommerce.Repository.Entity;
using Ecommerce.Repository;
using Ecommerce.Repository.Models;

public class OrderRepository : IOrderRepository
{
    private readonly ECommerceContext _context;

    public OrderRepository(ECommerceContext context)
    {
        _context = context;
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems) // İlgili OrderItems'i de dahil et
            .ThenInclude(oi => oi.Product) // OrderItems içindeki Product'ı dahil et
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task AddAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    
}
