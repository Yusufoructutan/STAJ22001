using Ecommerce.Repository.Entity;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedDate { get; set; }

    // Navigasyon Özellikleri
    public ICollection<CartItem> CartItems { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }

    // Product ve ProductCategory arasındaki bire bir ilişki
    public ProductCategory ProductCategory { get; set; }
}
