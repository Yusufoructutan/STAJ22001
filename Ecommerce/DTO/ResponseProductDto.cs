namespace Ecommerce.DTO
{
    public class ResponseProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public String ProductImage { get; set; }
        public List<ProductCategoryDto> ProductCategories { get; set; }
    }
}
