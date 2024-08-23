using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Ecommerce.Repository.Entity;
using Ecommerce.Repository;
using Ecommerce.Repository.Models;

public class OrderRepository :Repository<Order>,IOrderRepository
{
    private readonly ECommerceContext _context;

    public OrderRepository(ECommerceContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems) 
            .ThenInclude(oi => oi.Product) 
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task AddAsync(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));

        if (order.OrderItems == null)
            order.OrderItems = new List<OrderItem>(); 

        await _context.Orders.AddAsync(order);

        foreach (var item in order.OrderItems)
        {
            if (item != null && item.ProductId != 0)
            {
                _context.OrderItems.Add(item); 
            }
        }

        await _context.SaveChangesAsync();
    }
    public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .Where(o => o.UserId == userId)
            .ToListAsync();
    }



}
