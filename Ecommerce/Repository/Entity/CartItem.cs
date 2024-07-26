namespace Ecommerce.Repository.Entity
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
