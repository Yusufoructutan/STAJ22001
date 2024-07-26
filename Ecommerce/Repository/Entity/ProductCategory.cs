using Ecommerce.Repository.Entity;

public class ProductCategory
{
    public int ProductCategoryId { get; set; }
    public int ProductId { get; set; }
    public int CategoryId { get; set; }

    // Navigasyon Özellikleri
    public Product Product { get; set; }
    public Category Category { get; set; }
}