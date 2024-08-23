using Ecommerce.Repository.Entity;
using Ecommerce.Repository;
using System;
using System.Linq;
using System.Threading.Tasks;

public class OrderBusiness : IOrderBusiness
{
    private readonly ICartItemRepository _cartItemRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IRepository<Product> _repository;
   
    public OrderBusiness(IRepository<Product> repository, IOrderRepository orderRepository, ICartItemRepository cartItemRepository, IProductRepository productRepository)
    {
        _orderRepository = orderRepository;
        _cartItemRepository = cartItemRepository;
        _productRepository = productRepository;
        _repository= repository;    
       
    }

    public async Task<int> CreateOrderFromCartAsync(int userId)
    {
        var cartItems = await _cartItemRepository.GetCartItemsByUserIdAsync(userId);
        if (!cartItems.Any())
        {
            throw new InvalidOperationException("Sepetiniz boş. Sipariş oluşturulamaz.");
        }

        var allProducts = await _productRepository.GetAllAsync();
        var productIds = cartItems.Select(ci => ci.ProductId).Distinct();
        var products = allProducts.Where(p => productIds.Contains(p.ProductId)).ToList();

        var orderItems = new List<OrderItem>();

        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            TotalAmount = cartItems.Sum(ci => ci.Quantity * products.First(p => p.ProductId == ci.ProductId).Price),
            OrderItems = orderItems 
        };

        
        foreach (var cartItem in cartItems)
        {
            var product = products.FirstOrDefault(p => p.ProductId == cartItem.ProductId);
            if (product != null)
            {
                if (product.StockQuantity < cartItem.Quantity)
                {
                    throw new InvalidOperationException($"Yetersiz stok: {product.Name} urunü için yeterli stok yok.");
                }

                product.StockQuantity -= cartItem.Quantity;
                await _repository.UpdateAsync(product);

                orderItems.Add(new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = product.Price
                });
            }
        }

        await _orderRepository.AddAsync(order);

        await _cartItemRepository.ClearCartAsync(userId);

        return order.OrderId;
    }



    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _orderRepository.GetOrderByIdAsync(orderId);
    }

    public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
    {
        return await _orderRepository.GetOrdersByUserIdAsync(userId);
    }



}
