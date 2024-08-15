namespace Ecommerce.Repository
{
    public interface IProductRepository
    {
        // Belirli ID'lere sahip urunlerin listesini getiren metod

        Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId);

        Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds);
        Task<IEnumerable<Product>> GetAllAsync();



    }
}
