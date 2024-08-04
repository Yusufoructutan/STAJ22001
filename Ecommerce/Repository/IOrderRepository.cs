using Ecommerce.Repository.Entity;

namespace Ecommerce.Repository
{
    public interface IOrderRepository
    {


        // Belirtilen sipariş ID'sine sahip siparişi asenkron olarak getirir.
        Task<Order> GetOrderByIdAsync(int orderId);



        // Yeni bir siparişi asenkron olarak ekler.
        Task AddAsync(Order order);










    }
}
