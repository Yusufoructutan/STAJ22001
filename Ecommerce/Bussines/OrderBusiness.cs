using Ecommerce.Bussines;
using Ecommerce.Repository.Entity;
using Ecommerce.Repository;
using System.Linq;
using System.Threading.Tasks;

public class OrderBusiness : IOrderBusiness
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;

    public OrderBusiness(IOrderRepository orderRepository, ICartItemRepository cartItemRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _cartItemRepository = cartItemRepository;
        _productRepository = productRepository;
    }

    public async Task<int> CreateOrderFromCartAsync(int userId)
    {
        var cartItems = await _cartItemRepository.GetCartItemsByUserIdAsync(userId);
        var productIds = cartItems.Select(ci => ci.ProductId).Distinct();
        var products = await _productRepository.GetProductsByIdsAsync(productIds);

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            TotalAmount = cartItems.Sum(ci => ci.Quantity * products.First(p => p.ProductId == ci.ProductId).Price)
        };

        await _orderRepository.AddAsync(order);
        await _cartItemRepository.ClearCartAsync(userId);

        return order.OrderId;
    }

   

    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _orderRepository.GetOrderByIdAsync(orderId);
    }
}
