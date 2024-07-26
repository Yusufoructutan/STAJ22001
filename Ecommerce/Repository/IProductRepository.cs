using Ecommerce.Repository.Entity;
using System.Threading.Tasks;

namespace Ecommerce.Repository { 
public interface IProductRepository
{
        Task<IEnumerable<Product>> GetAllProductsAsync(); // Bu metodun tanımlı olduğundan emin olun
        Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds);

        Task<Product> GetByIdAsync(int id);
   
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
}}