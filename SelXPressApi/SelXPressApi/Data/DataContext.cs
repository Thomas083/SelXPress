using Microsoft.EntityFrameworkCore;
using SelXPressApi.Models;

namespace SelXPressApi.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    
    public DbSet<Attribut>Attributs { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Mark> Marks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // handle the many to many relationship between Order and Product
        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.ProductId, op.OrderId });
        modelBuilder.Entity<OrderProduct>()
            .HasOne(p => p.Product)
            .WithMany(op => op.OrderProducts)
            .HasForeignKey(pr => pr.ProductId);
        modelBuilder.Entity<OrderProduct>()
            .HasOne(o => o.Order)
            .WithMany(op => op.OrderProducts)
            .HasForeignKey(o => o.OrderId);

        //handle the amny to many relationship between User and Product

        modelBuilder.Entity<Cart>()
            .HasKey(c => new { c.ProductId, c.UserId });
        modelBuilder.Entity<Cart>()
            .HasOne(u => u.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserId);
        modelBuilder.Entity<Cart>()
            .HasOne(p => p.Product)
            .WithMany(p => p.Carts)
            .HasForeignKey(p => p.ProductId);

    }
}