using Ecommerce.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductBusiness
{
    // Tüm ürünleri DTO olarak al
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();

    // Belirli bir ürünün detaylarını Product olarak al
    Task<Product> GetProductByIdAsync(int id);

    // Yeni ürün ekle
    Task<int> AddProductAsync(ProductDto productDto);

    // Ürünü güncelle
    Task UpdateProductAsync(int productId, ProductDto updatedProductDto);

    // Ürünü sil
    Task DeleteProductAsync(int id);
}
