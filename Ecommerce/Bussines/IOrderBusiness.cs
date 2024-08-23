using Ecommerce.Repository.Entity;

public interface IOrderBusiness
{
    Task<int> CreateOrderFromCartAsync(int userId);
    Task<Order> GetOrderByIdAsync(int id);

    Task<List<Order>> GetOrdersByUserIdAsync(int userId); 

}
