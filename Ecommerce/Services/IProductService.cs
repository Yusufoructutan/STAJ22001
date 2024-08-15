using Ecommerce.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductService
{
        Task<IEnumerable<ResponseProductDto>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

    Task<ResponseProductDto> GetProductByIdAsync(int id);
    Task<int> AddProductAsync(ProductDto productDto);
    Task UpdateProductAsync(int productId, ProductDto updatedProductDto);
    Task DeleteProductAsync(int id);
}
