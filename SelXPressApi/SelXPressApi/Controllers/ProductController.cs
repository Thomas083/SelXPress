using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.ProductDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// API controller for managing product.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly IAuthorizationMiddleware _authorizationMiddleware;
		private readonly IProductRepository _productRepository;
		private readonly IMapper _mapper;
        private readonly DataContext _context;
		/// <summary>
		/// Initializes a new instance of the <see cref="ProductController"/> class.
		/// </summary>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations.</param>
		/// <param name="productRepository">The product repository to retrieve and manage products.</param>
		/// <param name="mapper">The AutoMapper instance for object mapping.</param>
		/// <param name="context">The data context for accessing the database.</param>
		public ProductController(IAuthorizationMiddleware authorizationMiddleware, IProductRepository productRepository, IMapper mapper, DataContext context)
		{
			_authorizationMiddleware = authorizationMiddleware;
			_productRepository = productRepository;
			_mapper = mapper;
			_context = context;
		}

		#region Get Methods
		/// <summary>
		/// Get all products with filter.
		/// </summary>
		/// <param name="categoryName">Filter by category name.</param>
		/// <param name="tagNames">Filter by tag names.</param>
		/// <param name="pageNumber">Page number.</param>
		/// <param name="pageSize">Page size.</param>
		/// <param name="sortBy">Sort by field.</param>
		/// <returns>Returns an array of products.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundErrorTemplate">Thrown when no products are found in the database.</exception>
		[HttpGet("filters")]
		[ProducesResponseType(200, Type = typeof(List<ProductDTO>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllProductsFilter(string categoryName, List<string> tagNames, int pageNumber, int pageSize, string sortBy)
		{
			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "PRO-1101");

			// Construct the query based on filters and sort options
			var query = _context.Products.Include(p => p.Category).Include(p => p.ProductAttributes)
								.Where(p => categoryName == null || p.Category.Name == categoryName)
								.Where(p => tagNames == null || tagNames.All(t => p.ProductAttributes.Any(pa => pa.Attribute.Name == t)));

			// Apply sorting
			if (!string.IsNullOrEmpty(sortBy))
			{
				switch (sortBy.ToLower())
				{
					case "name":
						query = query.OrderBy(p => p.Name);
						break;
					case "price":
						query = query.OrderBy(p => p.Price);
						break;
					default:
						break;
				}
			}
			else
			{
				query = query.OrderBy(p => p.Id);
			}

			// Retrieve products based on paging
			var totalCount = await query.CountAsync();
			var products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

			return Ok(products);
		}

		/// <summary>
		/// Get all products.
		/// </summary>
		/// <returns></returns>
		/// <returns>Returns an array of products.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundErrorTemplate">Thrown when no products are found in the database.</exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<AllProductDTO>))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllProduct()
		{
			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "PRO-1101");

			// Retrieve all products from the repository
			var products = await _productRepository.GetAllProducts();

			// Check if there are products in the database
			if (products == null)
				throw new NotFoundException("There are no products in the database, please try again", "PRO-1401");

			return Ok(products);
		}

		/// <summary>
		/// Get a product by ID.
		/// </summary>
		/// <param name="id">The ID of the product.</param>
		/// <returns>Returns the product with the specified ID.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundErrorTemplate">Thrown when the product with the specified ID doesn't exist.</exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(ProductDTO))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> Get(int id)
		{
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "PRO-1101");

			var product = await _productRepository.GetProductById(id);

			if (product == null)
				throw new NotFoundException("There are no products in the database", "PRO-1401");

			return Ok(product);
		}
		#endregion

		#region Post Methods
		/// <summary>
		/// Create a new product.
		/// </summary>
		/// <param name="product">The product to create.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the provided product data is incomplete.</exception>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO product)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin or seller role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
			{
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "PRO-2001");
			}

			// Check if the provided product data is complete
			if (product == null)
				throw new BadRequestException("The model is wrong, a bad request occurred", "PRO-1101");

			// Create the product using the repository
			await _productRepository.CreateProduct(product);
			return StatusCode(201);
		}
		#endregion

		#region Put Methods
		/// <summary>
		/// Update an existing product.
		/// </summary>
		/// <param name="id">The ID of the product to update.</param>
		/// <param name="product">The updated product data.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or the provided product update data is incomplete.</exception>
		/// <exception cref="NotFoundException">Thrown when the product with the specified ID doesn't exist.</exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO product)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin or seller role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
			{
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "PRO-2001");
			}

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "PRO-1101");

			// Check if the provided product update data is complete
			if (product == null)
				throw new BadRequestException("Some fields are missing, please try again with complete data", "PRO-1102");

			// Check if the product with the given ID exists
			if (!await _productRepository.ProductExists(id))
				throw new NotFoundException("The product with ID " + id + " doesn't exist", "PRO-1402");

			// Update the product using the repository
			await _productRepository.UpdateProduct(id, product);
			return Ok();
		}
		#endregion

		#region Delete Methods
		/// <summary>
		/// Delete an existing product.
		/// </summary>
		/// <param name="id">The ID of the product to delete.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the product with the specified ID doesn't exist.</exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin or seller role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
			{
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "PRO-2001");
			}

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "PRO-1101");

			// Check if the product with the given ID exists
			if (!await _productRepository.ProductExists(id))
				throw new NotFoundException("The product with ID " + id + " doesn't exist", "PRO-1402");

			// Delete the product using the repository
			await _productRepository.DeleteProduct(id);
			return Ok();
		}
		#endregion
	}
}
