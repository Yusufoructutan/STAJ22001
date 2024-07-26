using Ecommerce.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }





    // GET api/product/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductById(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product == null)
        {
            return NotFound(); // 404 Not Found
        }
        return Ok(product); // 200 OK
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        var products = await _productService.GetAllProductsAsync();
        return Ok(products);
    }

    // POST api/product
    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState); // 400 Bad Request
        }

        var productId = await _productService.AddProductAsync(productDto);

        var response = new
        {
            Message = "Ürün başarıyla eklendi.",
            ProductId = productId
        };

        return CreatedAtAction(nameof(GetProductById), new { id = productId }, response); // 201 Created
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        try
        {
            await _productService.DeleteProductAsync(id);
            return Ok(new { Message = "Ürün başarıyla silindi." });
        }
        catch (Exception ex)
        {
            // Log the exception
            // _logger.LogError(ex, "Error while deleting product with ID {id}", id);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto updatedProductDto)
    {
        if (updatedProductDto == null || id != updatedProductDto.ProductId)
        {
            return BadRequest("Ürün verileri geçersiz veya kimlik uyuşmazlığı yaşanıyor.");
        }

        try
        {
            await _productService.UpdateProductAsync(id, updatedProductDto);
            return Ok(new { Message = "Ürün başarıyla güncellendi." });
        }
        catch (Exception ex)
        {
            // Log the exception
            // _logger.LogError(ex, "Error while updating product with ID {id}", id);
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }



}
