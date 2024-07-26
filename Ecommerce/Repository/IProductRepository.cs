using Ecommerce.Repository.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public interface IProductRepository
    {
        // Ürünlerin tümünü getiren metod
        Task<IEnumerable<Product>> GetAllAsync();

        // Belirli ID'lere sahip ürünlerin listesini getiren metod
        Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds);

        // Belirli bir ID'ye sahip ürünü getiren metod
        Task<Product> GetByIdAsync(int id);

        // Yeni bir ürün ekleyen metod
        Task AddAsync(Product product);

        // Var olan ürünü güncelleyen metod
        Task UpdateAsync(Product product);

        // Ürünü silen metod
        Task DeleteAsync(int id);
    }
}
