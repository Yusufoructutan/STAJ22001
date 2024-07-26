using Ecommerce.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Repository.Entity;

namespace Ecommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ECommerceContext _context;

        public ProductRepository(ECommerceContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProductCategory) // Product ve ProductCategory ilişkisini yükle
                .ThenInclude(pc => pc.Category)   // Category'yi yükle
                .ToListAsync();
        }
        public async Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds)
        {
            return await _context.Products
                .Where(p => productIds.Contains(p.ProductId))
                .ToListAsync();
        }



        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductCategory) // Product ve ProductCategory ilişkisini yükle
                .ThenInclude(pc => pc.Category)   // Category'yi yükle
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.ProductCategory) // Product ve ProductCategory ilişkisini yükle
                .ThenInclude(pc => pc.Category)   // Category'yi yükle
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
}
