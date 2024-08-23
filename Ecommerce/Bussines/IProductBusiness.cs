using Ecommerce.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IProductBusiness
{
    Task<IEnumerable<ResponseProductDto>> GetAllProductsAsync();

    Task<Product> GetProductByIdAsync(int id);

    Task<int> AddProductAsync(ProductDto productDto);

    Task UpdateProductAsync(int productId, ProductDto updatedProductDto);

    Task DeleteProductAsync(int id);

    Task<List<ResponseProductDto>> SearchProductsAsync(string searchTerm);

}
