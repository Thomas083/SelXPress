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
    /// Crud operations for products
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
        /// <param name="authorizationMiddleware">Authorization middleware.</param>
        /// <param name="productRepository">Product repository.</param>
        /// <param name="mapper">Automapper instance.</param>
        /// <param name="context">Database context.</param>
        public ProductController(IAuthorizationMiddleware authorizationMiddleware, IProductRepository productRepository, IMapper mapper, DataContext context)
		{
			_authorizationMiddleware = authorizationMiddleware;
			_productRepository = productRepository;
			_mapper = mapper;
            _context = context;
		}

        /// <summary>
        /// Get all product attributes.
        /// </summary>
        /// <param name="categoryName">Name of the category to filter by.</param>
        /// <param name="tagNames">List of tag names to filter by.</param>
        /// <param name="pageNumber">Page number.</param>
        /// <param name="pageSize">Number of items per page.</param>
        /// <param name="sortBy">Field to sort by.</param>
        /// <returns>An array of all product attributes.</returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<ProductDTO>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAllProducts(string categoryName, List<string> tagNames, int pageNumber, int pageSize, string sortBy)
        {
            if (!ModelState.IsValid)
                throw new BadRequestException("The model is wrong, a bad request occurred", "PRO-1101");

            var query = _context.Products.Include(p => p.Category).Include(p => p.ProductAttributes)
                                .Where(p => categoryName == null || p.Category.Name == categoryName)
                                .Where(p => tagNames == null || tagNames.All(t => p.ProductAttributes.Any(pa => pa.Attribute.Name == t)));

            // Sorting
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
                    // Add more cases for other sorting options
                    default:
                        break;
                }
            }
            else
            {
                query = query.OrderBy(p => p.Id); // Default sorting
            }

            var totalCount = await query.CountAsync();
            var products = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(products);
        }

        /// <summary>
        /// Get details of a product by ID.
        /// </summary>
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
                throw new NotFoundException("There are no Product Attributes in the database", "PRO-1401");

            return Ok(product);
        }

        /// <summary>
        /// Create a new product.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDTO product)
        {
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
            if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) && 
                !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
            {
                throw new ForbiddenRequestException("You are not authorized to perform this operation", "PRO-2001");
            }

            if (product == null)
                throw new BadRequestException("The model is wrong, a bad request occurred", "PRO-1101");
            await _productRepository.CreateProduct(product);
            return StatusCode(201);
        }
        /// <summary>
        /// Update a product.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductDTO product)
        {
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
            if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
                                    !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
            {
                throw new ForbiddenRequestException("You are not authorized to perform this operation", "PRO-2001");
            }

            if (product == null)
                throw new BadRequestException("The model is wrong, a bad request occurred", "PRO-1101");

            if (!await _productRepository.ProductExists(id))
                throw new NotFoundException("There are no Product Attributes in the database", "PRO-1401");

            await _productRepository.UpdateProduct(id, product);
            return Ok();
        }

        /// <summary>
        /// Delete a product.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
            if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
                                                        !await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
            {
                throw new ForbiddenRequestException("You are not authorized to perform this operation", "PRO-2001");
            }

            if (!await _productRepository.ProductExists(id))
                throw new NotFoundException("There are no Product Attributes in the database", "PRO-1401");

            await _productRepository.DeleteProduct(id);
            return Ok();
        }
    }
}
