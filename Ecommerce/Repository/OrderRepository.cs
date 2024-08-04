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
            .Include(o => o.OrderItems) // İlgili OrderItems'i de dahil et
            .ThenInclude(oi => oi.Product) // OrderItems içindeki Product'ı dahil et
            .FirstOrDefaultAsync(o => o.OrderId == orderId);
    }

    public async Task AddAsync(Order order)
    {
        if (order == null)
            throw new ArgumentNullException(nameof(order));

        if (order.OrderItems == null)
            order.OrderItems = new List<OrderItem>(); // Eğer OrderItems null ise, boş bir liste oluşturun.

        // Order ve OrderItems'i veritabanına ekleyin
        await _context.Orders.AddAsync(order);

        // OrderItems'ı da ekleyin (Bağlantılar zaten kurulmuş olmalı)
        foreach (var item in order.OrderItems)
        {
            // Eğer item null olabilir veya item'in ProductId'si null olabilir, bu durumda kontrol ekleyin.
            if (item != null && item.ProductId != 0)
            {
                _context.OrderItems.Add(item); // OrderItems koleksiyonunu veritabanına ekleyin
            }
        }

        await _context.SaveChangesAsync();
    }



}
