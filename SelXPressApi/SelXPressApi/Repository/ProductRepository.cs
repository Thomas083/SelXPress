using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.ProductDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelXPressApi.Repository
{
	/// <summary>
	/// Repository for performing CRUD operations on Products.
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
	public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The database context. <see cref="DataContext"/></param>
        /// <param name="commonMethods">Common methods provider. <see cref="ICommonMethods"/></param>
        /// <param name="mapper">Automapper instance. <see cref="IMapper"/></param>
        public ProductRepository(DataContext context, ICommonMethods commonMethods, IMapper mapper)
        {
            _context = context;
            _commonMethods = commonMethods;
            _mapper = mapper;
        }

        /// <summary>
        /// Checks if a product with the specified ID exists.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns><c>true</c> if the product exists, otherwise <c>false</c>.</returns>
        public async Task<bool> ProductExists(int id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="createProduct">The product details.</param>
        /// <returns><c>true</c> if the product was created successfully, otherwise <c>false</c>.</returns>
        public async Task<bool> CreateProduct(CreateProductDTO createProduct, string email)
        {
	        var category = await _context.Categories.Where(c => c.Id == createProduct.CategoryId).FirstAsync();
            var newProduct = new Product
            {
				Name = createProduct.Name,
				Description = createProduct.Description,
				Price = createProduct.Price,
                Picture = createProduct.Picture,
                Stock = createProduct.Stock,
                Category = category,
                ProductAttributes = new List<ProductAttribute>()
			};

            var user = await _context.Users.Where(u => u.Email == email).FirstAsync();
            List<SellerProduct> sellerProducts = new List<SellerProduct>();
            SellerProduct sellerProduct = new SellerProduct()
            {
	            Product = newProduct,
	            User = user,
	            UserId = user.Id
            };
            
            sellerProducts.Add(sellerProduct);
            newProduct.SellerProducts = sellerProducts;
            for (int i = 0; i < createProduct.ProductAttributeIds.Count; i++)
            {
				var productAttribute = await _context.ProductAttributes.FindAsync(createProduct.ProductAttributeIds[i]);
                if (productAttribute != null)
				    newProduct.ProductAttributes.Add(productAttribute);
			}

            _context.Products.Add(newProduct);
            return await _commonMethods.Save();
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns><c>true</c> if the product was deleted successfully, otherwise <c>false</c>.</returns>
        public async Task<bool> DeleteProduct(int id)
        {
            if (await ProductExists(id))
            {
                var product = await _context.Products.FindAsync(id);
                _context.Products.Remove(product);
                await _context.Carts.Where(c => c.ProductId == product.Id).ExecuteDeleteAsync();
                await _context.OrderProducts.Where(op => op.ProductId == product.Id).ExecuteDeleteAsync();
                await _context.ProductAttributes.Where(pa => pa.ProductId == product.Id).ExecuteDeleteAsync();
                await _context.SellerProducts.Where(sp => sp.ProductId == product.Id).ExecuteDeleteAsync();
                return await _commonMethods.Save();
            }
            return false;
        }

		/// <summary>
		/// Retrieves a paginated list of products based on filtering criteria.
		/// </summary>
		/// <param name="categoryName">The name of the category to filter by.</param>
		/// <param name="tagNames">The list of tag names to filter by.</param>
		/// <param name="pageNumber">The page number.</param>
		/// <param name="pageSize">The number of items per page.</param>
		/// <returns>A list of products.</returns>
		public async Task<List<Product>> GetAllProductsFilters(string categoryName, List<string> tagNames, int pageNumber, int pageSize)
		{
			var query = _context.Products.Include(p => p.Category).Include(p => p.ProductAttributes)
							  .Where(p => categoryName == null || p.Category.Name == categoryName)
							  .Where(p => tagNames == null || tagNames.All(t => p.ProductAttributes.Any(pa => pa.Attribute.Name == t)))
							  .OrderBy(p => p.Id);

			var totalCount = await query.CountAsync();
			var products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

			return products;
		}

		/// <summary>
		/// Retrieves a list of all products along with their associated data.
		/// </summary>
		/// <returns>A list of products with associated data.</returns>
		public async Task<List<AllProductDTO>> GetAllProducts()
		{
			var query = _context.Products
				.Include(p => p.Category)
				.Include(p => p.ProductAttributes)
				.Include(c => c.Comments)
				.OrderBy(p => p.Id);

			var products = await query.ToListAsync();
			var productDTOs = _mapper.Map<List<AllProductDTO>>(products);

			return productDTOs;
		}


		/// <summary>
		/// Retrieves a product by its ID.
		/// </summary>
		/// <param name="id">The product ID.</param>
		/// <returns>The product if found, otherwise <c>null</c>.</returns>
		public async Task<AllProductDTO?> GetProductById(int id)
        {
            return _mapper.Map<AllProductDTO>(await _context.Products
                			.Include(p => p.Category)
                            .Include(p => p.ProductAttributes)
			                .Include(c => c.Comments)
                            .FirstOrDefaultAsync(p => p.Id == id));
        }

        /// <summary>
        /// Updates a product with new information.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="updateProductDTO">The updated product information.</param>
        /// <returns><c>true</c> if the product was updated successfully, otherwise <c>false</c>.</returns>
        public async Task<bool> UpdateProduct(int id, UpdateProductDTO updateProductDTO)
        {
            if (!await ProductExists(id))
                return false;
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
	            //_mapper.Map(updateProductDTO, product);
	            product.Name = updateProductDTO.Name;
	            product.Price = updateProductDTO.Price;
	            product.Description = updateProductDTO.Description;
	            product.Picture = updateProductDTO.Picture;
	            product.Stock = updateProductDTO.Stock;
	            var category = await _context.Categories.Where(c => c.Id == updateProductDTO.CategoryId).FirstAsync();
	            product.Category = category;
	            //récupérer la liste des products attributes
	            await _context.ProductAttributes.Where(pa => pa.ProductId == product.Id).ExecuteDeleteAsync();
	            List<ProductAttribute> productAttributeList = new List<ProductAttribute>();
	            for (int i = 0; i < updateProductDTO.AttributeIds.Count; i++)
	            {
		            var attribute = await _context.Attributes.Where(a => a.Id == updateProductDTO.AttributeIds[i]).FirstAsync();
		            var attributeProductToAdd = new ProductAttribute()
		            {
			            Product = product,
			            ProductId = product.Id,
			            Attribute = attribute,
			            AttributeId = attribute.Id
		            };
		            productAttributeList.Add(attributeProductToAdd);
	            }
	           
	            product.ProductAttributes = productAttributeList;
	            _context.Products.Update(product);
	            await _context.SaveChangesAsync();
	            return true;

            }
            return await _commonMethods.Save();
        }

		/// <summary>
		/// Retrieves a list of products associated with a user based on their email address.
		/// </summary>
		/// <param name="email">The email address of the user for whom to retrieve products.</param>
		/// <returns>A list of products associated with the specified user's email address, or an empty list if the user does not exist or has no products.</returns>
		public async Task<List<AllProductDTO>> GetProductByUser(string email)
		{
			// Check if a user with the specified email address exists in the database
			if (await _context.Users.Where(u => u.Email == email).AnyAsync())
			{
				// Retrieve the user with the specified email address
				var user = await _context.Users.Where(u => u.Email == email).FirstAsync();

				// Retrieve a list of seller products associated with the user
				var sellerProducts = await _context.SellerProducts.Where(sp => sp.UserId == user.Id).ToListAsync();

				var products = new List<Product>();

				// Iterate through the seller products and retrieve the corresponding products
				for (int i = 0; i < sellerProducts.Count; i++)
				{
					var productToAdd = await _context.Products.Where(p => p.Id == sellerProducts[i].ProductId).FirstAsync();
					products.Add(productToAdd);
				}

				// Use AutoMapper to map the list of products to a list of AllProductDTOs
				return _mapper.Map<List<AllProductDTO>>(products);
			}

			return new List<AllProductDTO>(); // Return an empty list if the user does not exist or has no products
		}

	}
}
