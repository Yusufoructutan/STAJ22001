using Ecommerce.DTO;
using Ecommerce.Business; // Doğru namespace
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Repository.Models;

namespace Ecommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductBusiness _productBusiness;
        private readonly ECommerceContext _context;

        public ProductService(IProductBusiness productBusiness, ECommerceContext context)
        {
            _productBusiness = productBusiness;
            _context = context;

        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.ProductCategory) // ProductCategory'yi de dahil et
                .ThenInclude(pc => pc.Category) // Category'yi de dahil et
                .Select(p => new ProductDto
                {
                    ProductId = p.ProductId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockQuantity = p.StockQuantity,
                    ProductCategories = p.ProductCategory != null
                        ? new List<ProductCategoryDto>
                        {
                        new ProductCategoryDto
                        {
                            ProductCategoryId = p.ProductCategory.ProductCategoryId,
                            CategoryId = p.ProductCategory.CategoryId,
                            CategoryName = p.ProductCategory.Category.Name // Kategori adını al
                        }
                        }
                        : new List<ProductCategoryDto>()
                })
                .ToListAsync();
        }







        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _productBusiness.GetProductByIdAsync(id);

            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                // Tekil ProductCategoryDto oluşturma
                ProductCategories = product.ProductCategory != null
                    ? new List<ProductCategoryDto>
                    {
                new ProductCategoryDto
                {
                    ProductCategoryId = product.ProductCategory.ProductCategoryId,
                    CategoryId = product.ProductCategory.CategoryId,
                    CategoryName = product.ProductCategory.Category.Name
                }
                    }
                    : new List<ProductCategoryDto>() // Boş liste döndürmek
            };
        }



        public async Task<int> AddProductAsync(ProductDto productDto)
        {
            // ProductDto'yu işleyerek iş katmanına ilet
            return await _productBusiness.AddProductAsync(productDto);
        }

        public async Task UpdateProductAsync(ProductDto productDto)
        {
            // ProductDto'yu işleyerek iş katmanına ilet
            await _productBusiness.UpdateProductAsync(productDto);
        }

        public async Task DeleteProductAsync(int id)
        {
            // Ürünü silmek için iş katmanına ilet
            await _productBusiness.DeleteProductAsync(id);
        }
    }
}
