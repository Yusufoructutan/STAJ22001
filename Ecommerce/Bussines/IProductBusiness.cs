using Ecommerce.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductBusiness
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync(); // Bu metodu ekleyin

    Task<Product> GetProductByIdAsync(int id);
    Task<int> AddProductAsync(ProductDto productDto);
    Task UpdateProductAsync(ProductDto productDto);
    Task DeleteProductAsync(int id);
}
