namespace Ecommerce.Repository
{
    public interface IProductRepository
    {

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

        Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<List<Product>> SearchProductsAsync(string searchTerm);




    }
}
