using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.ProductAttributeDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// API controller for managing product attributes.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class ProductAttributeController : ControllerBase
	{
		private readonly IAuthorizationMiddleware _authorizationMiddleware;
		private readonly IProductAttributeRepository _productAttributeRepository;
		private readonly IMapper _mapper;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProductAttributeController"/> class.
		/// </summary>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations.</param>
		/// <param name="productAttributeRepository">The product attribute repository to retrieve and manage product attributes.</param>
		/// <param name="mapper">The AutoMapper instance for object mapping.</param>
		public ProductAttributeController(IAuthorizationMiddleware authorizationMiddleware, IProductAttributeRepository productAttributeRepository, IMapper mapper)
		{
			_authorizationMiddleware = authorizationMiddleware;
			_productAttributeRepository = productAttributeRepository;
			_mapper = mapper;
		}

		#region Get Methods
		/// <summary>
		/// Get all product attributes.
		/// </summary>
		/// <returns>Returns an array of all product attributes.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when no product attributes are found in the database.</exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<ProductAttributeDTO>))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetProductAttributes()
		{
			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "PAT-1101");

			// Retrieve all product attributes from the repository
			var productAttributes = await _productAttributeRepository.GetAllProductAttributes();

			// Check if any product attributes were found
			if (productAttributes.Count == 0)
				throw new NotFoundException("There are no product attributes in the database", "PAT-1401");

			return Ok(productAttributes);
		}

		/// <summary>
		/// Get a product attribute based on the ID.
		/// </summary>
		/// <param name="id">The ID of the product attribute.</param>
		/// <returns>Returns the product attribute with the specified ID.</returns>
		/// <exception cref="NotFoundException">Thrown when the product attribute with the specified ID doesn't exist.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(ProductAttributeDTO))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetProductAttribute(int id)
		{
			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "PAT-1101");

			// Retrieve the product attribute by its ID
			var productAttribute = await _productAttributeRepository.GetProductAttributeById(id);

			// Check if the product attribute was found
			if (productAttribute == null)
				throw new NotFoundException($"The product attribute with the ID : {id} doesn't exist", "PAT-1402");

			return Ok(productAttribute);
		}
		#endregion

		#region Post Method
		/// <summary>
		/// Create a new product attribute.
		/// </summary>
		/// <param name="productAttribute">The product attribute to create.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the provided product attribute data is incomplete.</exception>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateProductAttribute([FromBody] CreateProductAttributeDTO productAttribute)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin or seller role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
			{
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "PAT-2001");
			}

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "PAT-1101");

			// Create the product attribute using the repository
			await _productAttributeRepository.CreateProductAttribute(productAttribute);
			return StatusCode(201);
		}
		#endregion

		#region Put Method
		/// <summary>
		/// Update an existing product attribute.
		/// </summary>
		/// <param name="id">The ID of the product attribute to update.</param>
		/// <param name="updateProductAttribute">The updated product attribute data.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or the provided product attribute update data is incomplete.</exception>
		/// <exception cref="NotFoundException">Thrown when the product attribute with the specified ID doesn't exist.</exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateProductAttribute(int id, [FromBody] UpdateProductAttributeDTO updateProductAttribute)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin or seller role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
			{
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "PAT-2001");
			}

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "PAT-1101");

			// Check if the provided product attribute update data is complete
			if (updateProductAttribute == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "PAT-1102");

			// Check if the product attribute with the given ID exists
			if (!await _productAttributeRepository.ProductAttributeExists(id))
				throw new NotFoundException($"The product attribute with the ID : {id} doesn't exist", "PAT-1402");

			// Update the product attribute using the repository
			var result = await _productAttributeRepository.UpdateProductAttribute(id, updateProductAttribute);

			return Ok();
		}
		#endregion

		#region Delete Method
		/// <summary>
		/// Delete an existing product attribute.
		/// </summary>
		/// <param name="id">The ID of the product attribute to delete.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the product attribute with the specified ID doesn't exist.</exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteProductAttribute(int id)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin or seller role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfSeller(HttpContext))
			{
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "PAT-2001");
			}

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "PAT-1101");

			// Check if the product attribute with the given ID exists
			if (!await _productAttributeRepository.ProductAttributeExists(id))
				throw new NotFoundException($"The product attribute with the ID : {id} doesn't exist", "PAT-1402");

			// Delete the product attribute using the repository
			await _productAttributeRepository.DeleteProductAttribute(id);
			return Ok();
		}
		#endregion
	}
}
