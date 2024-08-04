namespace Ecommerce.Repository
{
    public interface IProductRepository
    {
        // Belirli ID'lere sahip ürünlerin listesini getiren metod
        
        
        Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds);
        Task<IEnumerable<Product>> GetAllAsync();



    }
}
