using Ecommerce.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductService
{
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(); // Metodu ekleyin

    Task<ProductDto> GetProductByIdAsync(int id);
    Task<int> AddProductAsync(ProductDto productDto);
    Task UpdateProductAsync(ProductDto productDto);
    Task DeleteProductAsync(int id);
}
