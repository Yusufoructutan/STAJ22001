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

        // Yalnızca GetAllAsync yöntemini kullanacağız
        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.ProductCategory) // Product ve ProductCategory ilişkisini yükle
                .ThenInclude(pc => pc.Category)   // Category'yi yükle
                .ToListAsync();
        }



        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.ProductCategory) // Product ve ProductCategory ilişkisini yükle
                .ThenInclude(pc => pc.Category)   // Category'yi yükle
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

        // Opsiyonel: Product'ların belirli bir liste id'sine sahip olanlarını almak için ek bir yöntem
        public async Task<List<Product>> GetProductsByIdsAsync(IEnumerable<int> productIds)
        {
            if (productIds == null || !productIds.Any())
            {
                return new List<Product>(); // Boş liste döndür
            }

            try
            {
                return await _context.Products
                    .Where(p => productIds.Contains(p.ProductId))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Hata loglama
                Console.WriteLine($"Hata: {ex.Message}");
                throw; // Hatanın üst seviyeye iletilmesini sağlar
            }
        }
    }
}
