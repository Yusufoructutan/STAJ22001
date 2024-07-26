using Ecommerce.Repository.Entity;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Repository.Models
{
    public class ECommerceContext : DbContext
    {
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<ProductCategory>().ToTable("ProductCategories");
            modelBuilder.Entity<CartItem>().ToTable("CartItems");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");

            // Users ve CartItems arasındaki birden çoğa ilişki
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(ci => ci.UserId);

            // Users ve Orders arasındaki birden çoğa ilişki
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            // ProductCategories ve Products arasındaki bire bir ilişki
            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithOne(p => p.ProductCategory)
                .HasForeignKey<ProductCategory>(pc => pc.ProductId);

            // ProductCategories ve Categories arasındaki bire bir ilişki
            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithOne(c => c.ProductCategory)
                .HasForeignKey<ProductCategory>(pc => pc.CategoryId);

            // Products ve CartItems arasındaki birden çoğa ilişki
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.CartItems)
                .HasForeignKey(ci => ci.ProductId);

            // Orders ve OrderItems arasındaki birden çoğa ilişki
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            // Products ve OrderItems arasındaki birden çoğa ilişki
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);
        }
    }
}
