using Ecommerce.Repository.Models;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Repository.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ECommerceContext _context;

        public ProductRepository(ECommerceContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.ProductCategory) 
                .ThenInclude(pc => pc.Category)   
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.ProductCategory.CategoryId == categoryId)
                .ToListAsync();
        }



        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductCategory) 
                .ThenInclude(pc => pc.Category)   
                .FirstOrDefaultAsync(p => p.ProductId == id);
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
        public async Task<List<Product>> SearchProductsAsync(string searchTerm)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(searchTerm))
                .ToListAsync();
        }


        public async Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds)
        {
            if (productIds == null || !productIds.Any())
            {
                return new List<Product>(); 
            }

            try
            {
                return await _context.Products
                    .Where(p => productIds.Contains(p.ProductId))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata: {ex.Message}");
                throw; 
            }
        }
    }
}
