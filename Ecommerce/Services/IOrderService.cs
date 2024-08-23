using Ecommerce.DTO;

namespace Ecommerce.Services
{
    public interface IOrderService
    {
        Task<int> CreateOrderFromCartAsync(int userId);
        Task<OrderDto> GetOrderByIdAsync(int id);

        Task<List<OrderDto>> GetOrdersByUserIdAsync(int userId); 

    }
}
