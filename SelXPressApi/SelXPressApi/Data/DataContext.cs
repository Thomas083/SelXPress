using Microsoft.EntityFrameworkCore;
using SelXPressApi.Models;
using Attribute = SelXPressApi.Models.Attribute;

namespace SelXPressApi.Data
{
	/// <summary>
	/// Represents the database context for SelXPressApi application.
	/// </summary>
	/// <seealso  cref="Models"/>
	/// <seealso  cref="DTO"/>
	/// <seealso  cref="Controllers"/>
	/// <seealso  cref="Repository"/>
	/// <seealso  cref="Helper"/>
	/// <seealso  cref="DocumentationErrorTemplate"/>
	/// <seealso  cref="Exceptions"/>
	/// <seealso  cref="Interfaces"/>
	/// <seealso  cref="Middleware"/>
	/// <seealso  cref="Data"/>
	public class DataContext : DbContext
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DataContext"/> class.
		/// </summary>
		/// <param name="options">The options for configuring the context.</param>
		public DataContext(DbContextOptions<DataContext> options) : base(options)
		{
		}

		// DbSet properties for different entities
		/// <summary>
		/// Gets or sets the DbSet for managing attributes in the database.
		/// Here you can access to Attribute controller <see cref="Controllers.AttributeController"/>
		/// </summary>
		public DbSet<Attribute> Attributes { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing attribute data in the database. 
		/// Here you can access to AttributeData controller <see cref="Controllers.AttributeDataController"/>
		/// </summary>
		public DbSet<AttributeData> AttributesData { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing carts in the database.
		/// Here you can access to Cart controller <see cref="Controllers.CartController"/>
		/// </summary>
		public DbSet<Cart> Carts { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing categories in the database.
		/// Here you can access to Category controller <see cref="Controllers.CategoryController"/>
		/// </summary>
		public DbSet<Category> Categories { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing comments in the database.
		/// Here you can access to Comment controller <see cref="Controllers.CommentController"/>
		/// </summary>
		public DbSet<Comment> Comments { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing marks in the database.
		/// Here you can access to Mark controller <see cref="Controllers.MarkController"/>
		/// </summary>
		public DbSet<Mark> Marks { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing orders in the database.
		/// Here you can access to Order controller <see cref="Controllers.OrderController"/>
		/// </summary>
		public DbSet<Order> Orders { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing order products in the database.
		/// Here you can access to OrderProduct controller <see cref="Controllers.OrderProductController"/>
		/// </summary>
		public DbSet<OrderProduct> OrderProducts { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing products in the database.
		/// Here you can access to Product controller <see cref="Controllers.ProductController"/>
		/// </summary>
		public DbSet<Product> Products { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing product attributes in the database.
		/// Here you can access to ProductAttribute controller <see cref="Controllers.ProductAttributeController"/>
		/// </summary>
		public DbSet<ProductAttribute> ProductAttributes { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing roles in the database.
		/// Here you can access to Role controller <see cref="Controllers.RoleController"/>
		/// </summary>
		public DbSet<Role> Roles { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing seller products in the database.
		/// Here you can access to SellerProduct controller <see cref="Controllers.SellerProductController"/>
		/// </summary>
		public DbSet<SellerProduct> SellerProducts { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing tags in the database.
		/// Here you can access to Tag controller <see cref="Controllers.TagController"/>
		/// </summary>
		public DbSet<Tag> Tags { get; set; }
		/// <summary>
		/// Gets or sets the DbSet for managing users in the database.
		/// Here you can access to User controller <see cref="Controllers.UserController"/>
		/// </summary>
		public DbSet<User> Users { get; set; }

		/// <summary>
		/// Configures the many-to-many relationships and entity associations.
		/// </summary>
		/// <param name="modelBuilder">The model builder to configure entities and relationships.</param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure many-to-many relationship between Order and Product
			modelBuilder.Entity<OrderProduct>()
				.HasKey(op => op.Id);
			modelBuilder.Entity<OrderProduct>()
				.HasOne(o => o.Order)
				.WithMany(op => op.OrderProducts)
				.HasForeignKey(o => o.OrderId);
			modelBuilder.Entity<OrderProduct>()
				.HasOne(p => p.Product)
				.WithMany(op => op.OrderProducts)
				.HasForeignKey(p => p.ProductId);

			// Configure many-to-many relationship between User and Product
			modelBuilder.Entity<Cart>()
				.HasKey(c => c.Id);
			modelBuilder.Entity<Cart>()
				.HasOne(p => p.Product)
				.WithMany(c => c.Carts)
				.HasForeignKey(p => p.ProductId);
			modelBuilder.Entity<Cart>()
				.HasOne(u => u.User)
				.WithMany(c => c.Carts)
				.HasForeignKey(u => u.UserId);

			// Configure many-to-many relationship between Product and Attribute
			modelBuilder.Entity<ProductAttribute>()
				.HasKey(pa => pa.Id);
			modelBuilder.Entity<ProductAttribute>()
				.HasOne(pa => pa.Attribute)
				.WithMany(a => a.ProductAttributes)
				.HasForeignKey(pa => pa.AttributeId);
			modelBuilder.Entity<ProductAttribute>()
				.HasOne(pa => pa.Product)
				.WithMany(p => p.ProductAttributes)
				.HasForeignKey(pa => pa.ProductId);

			// Configure Attribute and AttributeData relationship
			modelBuilder.Entity<Attribute>()
				.HasMany(a => a.AttributeData)
				.WithOne(ad => ad.Attribute)
				.HasForeignKey(ad => ad.AttributeId);
			
			// Configure User and product relationship with the SellerProduct Entity
			modelBuilder.Entity<SellerProduct>()
				.HasKey(sp => sp.Id);
			modelBuilder.Entity<SellerProduct>()
				.HasOne(p => p.Product)
				.WithMany(p => p.SellerProducts)
				.HasForeignKey(p => p.ProductId);
			modelBuilder.Entity<SellerProduct>()
				.HasOne(u => u.User)
				.WithMany(u => u.SellerProducts)
				.HasForeignKey(u => u.UserId);
		}
	}
}
