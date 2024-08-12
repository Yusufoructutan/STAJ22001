using Ecommerce.DTO;
using Ecommerce.Repository;
using Ecommerce.Repository.Entity;
using Ecommerce.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ProductBusiness : IProductBusiness
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ProductCategory> _productCategoryRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly ECommerceContext _context;

    public ProductBusiness(IRepository<Product> productRepository, IRepository<ProductCategory> productCategoryRepository, ECommerceContext context,IRepository<Category> categoryRepository)
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

    public async Task<IEnumerable<ResponseProductDto>> GetAllProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();

        return products.Select(p => new ResponseProductDto
        {
            ProductId = p.ProductId,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            StockQuantity = p.StockQuantity,
            // ProductCategoryDto ekleyin

        });
    }

    public async Task<int> AddProductAsync(ProductDto productDto)
    {
        var product = new Product
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            StockQuantity = productDto.StockQuantity,
            ProductImage=productDto.ProductImage
        };

        await _productRepository.AddAsync(product);
        await _context.SaveChangesAsync();

        foreach (var categoryDto in productDto.ProductCategories)
        {
            var category = await _categoryRepository.GetAsync(c => c.Name == categoryDto.CategoryName);
            if (category == null)
            {
                throw new Exception($"Kategori '{categoryDto.CategoryName}' bulunamadı");
            }

            var productCategory = new ProductCategory
            {
                ProductId = product.ProductId,
                CategoryId = category.CategoryId
            };

            await _productCategoryRepository.AddAsync(productCategory);
        }

        return product.ProductId;
    }

    public async Task UpdateProductAsync(int productId, ProductDto updatedProductDto)
    {
        var product = await _context.Products
            .Include(p => p.ProductCategory)
            .FirstOrDefaultAsync(p => p.ProductId == productId);

        if (product == null)
        {
            throw new Exception("Product not found");
        }

        product.Name = updatedProductDto.Name;
        product.Description = updatedProductDto.Description;
        product.Price = updatedProductDto.Price;
        product.StockQuantity = updatedProductDto.StockQuantity;

        _context.ProductCategories.RemoveRange(product.ProductCategory);

        foreach (var category in updatedProductDto.ProductCategories)
        {
            var productCategory = new ProductCategory
            {
                ProductId = product.ProductId,
                CategoryId = category.CategoryId
            };

            _context.ProductCategories.Add(productCategory);
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }
}
