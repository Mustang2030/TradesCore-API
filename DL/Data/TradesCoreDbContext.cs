using Data_Layer.Models;
using Microsoft.EntityFrameworkCore;

namespace Data_Layer.Data
{
    public class TradesCoreDbContext(DbContextOptions<TradesCoreDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(product =>
            {
                product.HasKey(p => p.Id);

                product.HasMany(reviews => reviews.Reviews)
                       .WithOne(product => product.Product)
                       .HasForeignKey(review => review.ProductId)
                       .OnDelete(DeleteBehavior.Cascade)
                       .IsRequired();
            });

            modelBuilder.Entity<Category>(category =>
            {
                category.HasKey(c => c.Id);

                category.HasMany(products => products.Products)
                        .WithMany(categories => categories.Categories)
                        .UsingEntity(cp => cp.ToTable("CategoryProducts"));
            });

            modelBuilder.Entity<Cart>(cart =>
            {
                cart.HasKey(c => c.Id);

                cart.HasOne(user => user.User)
                    .WithOne(cart => cart.Cart)
                    .HasForeignKey<Cart>(cart => cart.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                cart.HasMany(items => items.Items)
                    .WithMany(carts => carts.Carts)
                    .UsingEntity(ci => ci.ToTable("CartItems"));
            });

            modelBuilder.Entity<Review>(review =>
            {
                review.HasKey(r => r.Id);

                review.HasOne(p => p.Product)
                      .WithMany(r => r.Reviews)
                      .HasForeignKey(k => k.ProductId)
                      .OnDelete(DeleteBehavior.Cascade);

                review.HasOne(u => u.User)
                      .WithMany(r => r.Reviews)
                      .HasForeignKey(k => k.UserId)
                      .OnDelete(DeleteBehavior.NoAction);
            });
                

            modelBuilder.Entity<Order>(order =>
            {
                order.HasKey(o => o.Id);

                order.HasOne(payment => payment.Payment)
                     .WithOne(order => order.Order)
                     .HasForeignKey<Payment>(payment => payment.OrderId)
                     .OnDelete(DeleteBehavior.Restrict);

                order.HasMany(products => products.Items)
                     .WithMany(orders => orders.Orders)
                     .UsingEntity(oi => oi.ToTable("OrderItems"));

                order.Property(o => o.Status)
                     .HasConversion<string>()
                     .HasDefaultValue(OrderStatus.Pending);
            });

            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.Id);

                user.HasMany(orders => orders.Orders)
                    .WithOne(user => user.User)
                    .HasForeignKey(orders => orders.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                user.HasMany(reviews => reviews.Reviews)
                    .WithOne(user => user.User)
                    .HasForeignKey(review => review.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
            });

        }
    }
}
