namespace Ecommerce.DTO
{
    public class ProductDto
    {
       // public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ProductImage { get; set; }
        public List<ProductCategoryDto> ProductCategories { get; set; }



    }
}
