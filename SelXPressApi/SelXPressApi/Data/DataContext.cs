using Microsoft.EntityFrameworkCore;
using SelXPressApi.Models;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.Data;

public class DataContext : DbContext
{
    /// <summary>
    /// Constructor of the DataContext Class
    /// </summary>
    /// <param name="options"></param>
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }

    public DbSet<Attribute> Attributes { get; set; }
    public DbSet<AttributeData> AttributesData { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Mark> Marks { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderProduct> OrderProducts { get; set; }
    public DbSet<Product>  Products { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }
    
    
    /// <summary>
    /// method to handle the many to many relationship beetwen several tables
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // handle the many to many relationship between Order and Product
        modelBuilder.Entity<OrderProduct>()
            .HasKey(op => new { op.OrderId, op.ProductId });
        modelBuilder.Entity<OrderProduct>()
            .HasOne(o => o.Order)
            .WithMany(op => op.OrderProducts)
            .HasForeignKey(o => o.OrderId);
        modelBuilder.Entity<OrderProduct>()
            .HasOne(p => p.Product)
            .WithMany(op => op.OrderProducts)
            .HasForeignKey(p => p.ProductId);

        //handle the amny to many relationship between User and Product

        modelBuilder.Entity<Cart>()
            .HasKey(c => new { c.ProductId, c.UserId });
        modelBuilder.Entity<Cart>()
            .HasOne(p => p.Product)
            .WithMany(c => c.Carts)
            .HasForeignKey(p => p.ProductId);
        modelBuilder.Entity<Cart>()
            .HasOne(u => u.User)
            .WithMany(c => c.Carts)
            .HasForeignKey(u => u.UserId);

        //handle the many to many relationship between Product and Attribute

        modelBuilder.Entity<ProductAttribute>()
            .HasKey(pa => new { pa.ProductId, pa.AttributeId });
        modelBuilder.Entity<ProductAttribute>()
            .HasOne(pa => pa.Attribute)
            .WithMany(a => a.ProductAttributes)
            .HasForeignKey(pa => pa.AttributeId);
        modelBuilder.Entity<ProductAttribute>()
            .HasOne(pa => pa.Product)
            .WithMany(p => p.ProductAttributes)
            .HasForeignKey(pa => pa.ProductId);

        modelBuilder.Entity<Attribute>()
        .HasMany(a => a.AttributeData)
        .WithOne(ad => ad.Attribute)
        .HasForeignKey(ad => ad.AttributeId);
    }
}