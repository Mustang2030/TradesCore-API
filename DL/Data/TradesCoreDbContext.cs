using Data_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer.Data
{
    public class TradesCoreDbContext(DbContextOptions<TradesCoreDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryProduct>()
                .HasKey(cp => new { cp.CategoryId, cp.ProductId });

            modelBuilder.Entity<CategoryProduct>()
                .HasOne(cat => cat.Category)
                .WithMany(prods => prods.Products)
                .HasForeignKey(cat => cat.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CategoryProduct>()
                .HasOne(prod => prod.Product)
                .WithMany(cats => cats.Categories)
                .HasForeignKey(prod => prod.ProductId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Cart>()
                .HasOne(u => u.User)
                .WithOne()
                .HasForeignKey<Cart>(u => u.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItems>()
                .HasKey(Ci => new { Ci.CartId, Ci.ProductId });

            modelBuilder.Entity<CartItems>()
                .HasOne(c => c.Cart)
                .WithMany(ci => ci.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CartItems>()
                .HasOne(p => p.Product)
                .WithMany()
                .HasForeignKey(k => k.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Review>()
                .HasOne(p => p.Product)
                .WithMany(r => r.Reviews)
                .HasForeignKey(k => k.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Review>()
                .HasOne(u => u.User)
                .WithMany(r => r.Reviews)
                .HasForeignKey(k => k.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItems>()
                .HasKey(oi => new { oi.OrderId, oi.ProductId });

            modelBuilder.Entity<OrderItems>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<OrderItems>()
               .HasOne(oi => oi.Order)
               .WithMany(o => o.OrderItems)
               .HasForeignKey(oi => oi.OrderId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
               .HasOne(o => o.User)
               .WithMany(u => u.Orders)
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
