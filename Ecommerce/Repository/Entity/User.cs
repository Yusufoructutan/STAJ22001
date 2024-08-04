using System.Data;

namespace Ecommerce.Repository.Entity
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Role { get; set; }





        // Navigasyon Özellikleri
        public ICollection<CartItem> CartItems { get; set; }
        public ICollection<Order> Orders { get; set; }

    }
}
