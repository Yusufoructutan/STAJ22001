using Ecommerce.Repository.Entity;

namespace Ecommerce.Repository
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int orderId);
        Task AddAsync(Order order);
    }
}
