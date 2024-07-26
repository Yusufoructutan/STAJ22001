using Ecommerce.Repository;
using Ecommerce.Repository.Entity;
using Ecommerce.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Repository.Models;
public class ProductBusiness : IProductBusiness
{
    private readonly IProductRepository _productRepository;
    private readonly IRepository<ProductCategory> _productCategoryRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly ECommerceContext _context;

    public ProductBusiness(IProductRepository productRepository,IRepository<ProductCategory> productCategoryRepository, ECommerceContext context,
        IRepository<Category> categoryRepository)
    {
        _productRepository = productRepository;
        _productCategoryRepository = productCategoryRepository;
        _categoryRepository = categoryRepository;
        _context = context;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await _productRepository.GetByIdAsync(id);
    }



    public async Task<int> AddProductAsync(ProductDto productDto)
    {
        // Ürün bilgilerini oluştur
        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            StockQuantity = productDto.StockQuantity
        };

        // Ürünü veritabanına ekle
        await _productRepository.AddAsync(product);
        await _context.SaveChangesAsync(); // Veritabanı işlemlerini kaydet

        // Kategorileri ekle ve ilişkilendir
        foreach (var categoryDto in productDto.ProductCategories)
        {
            // Kategori adını kullanarak ilgili kategoriyi bul
            var category = await _categoryRepository.GetAsync(c => c.Name == categoryDto.CategoryName);

            if (category == null)
            {
                // Kategori bulunamazsa uygun bir hata yönetimi yapabilirsiniz
                throw new Exception($"Kategori '{categoryDto.CategoryName}' bulunamadı");
            }

            // Kategori ile ürünü ilişkilendir
            var productCategory = new ProductCategory
            {
                ProductId = product.ProductId,
                CategoryId = category.CategoryId
            };

            await _productCategoryRepository.AddAsync(productCategory);
        }

        // Ürün ID'sini döndür
        return product.ProductId;
    }




    public async Task UpdateProductAsync(ProductDto productDto)
    {
        var product = new Product
        {
            ProductId = productDto.ProductId,
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price
        };
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllProductsAsync();

        return products.Select(p => new ProductDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            // ProductCategoryDto ekleyin
           
        });
    }

}