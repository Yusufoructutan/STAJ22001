using Ecommerce.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductBusiness
{
    // Tüm urunleri DTO olarak al
    Task<IEnumerable<ResponseProductDto>> GetAllProductsAsync();

    // Belirli bir urunün detaylarını Product olarak al
    Task<Product> GetProductByIdAsync(int id);

    // Yeni urun ekle
    Task<int> AddProductAsync(ProductDto productDto);

    // urunü güncelle
    Task UpdateProductAsync(int productId, ProductDto updatedProductDto);

    // urunü sil
    Task DeleteProductAsync(int id);
}
