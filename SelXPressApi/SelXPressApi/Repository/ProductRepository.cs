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
    /// Repository for performing CRUD operations on products.
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        private readonly ICommonMethods _commonMethods;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductRepository"/> class.
        /// </summary>
        /// <param name="context">The database context.</param>
        /// <param name="commonMethods">Common methods provider.</param>
        /// <param name="mapper">Automapper instance.</param>
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
        public async Task<bool> CreateProduct(CreateProductDTO createProduct)
        {
            var productEntity = _mapper.Map<Product>(createProduct);

            // Create ProductAttributes based on the provided AttributeIds
            if (createProduct.AttributeIds != null && createProduct.AttributeIds.Any())
            {
                var productAttributes = createProduct.AttributeIds.Select(attributeId => new ProductAttribute
                {
                    AttributeId = attributeId
                }).ToList();

                productEntity.ProductAttributes = productAttributes;
            }

            _context.Products.Add(productEntity);
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
		public async Task<Product?> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
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

            _mapper.Map(updateProductDTO, product);            

            return await _commonMethods.Save(); ;
        }

        public async Task<List<Product>> GetProductByUser(string email)
        {
	        if (await _context.Users.Where(u => u.Email == email).AnyAsync())
	        {
		        var user = await _context.Users.Where(u => u.Email == email).FirstAsync();
		        var products = new List<Product>();
		        // a decommenter lorsque les utilisateurs seront présent dans le model des produits
		        //var products = await _context.Products.Where(p = p.User == user).ToListAsync();
		        return products;
	        }
	        return null;
        }
    }
}
