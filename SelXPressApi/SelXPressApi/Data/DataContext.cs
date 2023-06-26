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
        
        //handle the amny to many relationship between User and Product
        
    }
}