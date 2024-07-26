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
        // Kullanıcının sepetindeki ürünleri al
        var cartItems = await _cartItemRepository.GetCartItemsByUserIdAsync(userId);

        // Sepet boşsa hata döndür
        if (!cartItems.Any())
        {
            throw new InvalidOperationException("Sepetiniz boş. Sipariş oluşturulamaz.");
        }

        // Ürün ID'lerinden ürün bilgilerini al
        var productIds = cartItems.Select(ci => ci.ProductId).Distinct();
        var products = await _productRepository.GetProductsByIdsAsync(productIds);

        // Sipariş oluştur
        var order = new Order
        {
            UserId = userId,
            OrderDate = DateTime.Now,
            TotalAmount = cartItems.Sum(ci => ci.Quantity * products.First(p => p.ProductId == ci.ProductId).Price) // Toplam tutar hesapla
        };

        // Sipariş ekle
        await _orderRepository.AddAsync(order);

        // Sepeti temizle
        await _cartItemRepository.ClearCartAsync(userId);

        // Oluşturulan siparişin ID'sini döndür
        return order.OrderId;
    }




    public async Task<Order> GetOrderByIdAsync(int orderId)
    {
        return await _orderRepository.GetOrderByIdAsync(orderId);
    }
}
