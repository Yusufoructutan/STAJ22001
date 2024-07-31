using Ecommerce.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductService
{
        Task<IEnumerable<ProductDto>> GetAllProductsAsync(); 

    Task<ProductDto> GetProductByIdAsync(int id);
    Task<int> AddProductAsync(ProductDto productDto);
    Task UpdateProductAsync(int productId, ProductDto updatedProductDto);
    Task DeleteProductAsync(int id);
}
