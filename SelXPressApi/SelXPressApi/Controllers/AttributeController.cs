using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.AttributeDTO;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// API controller for managing attributes.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class AttributeController : ControllerBase
	{
		private readonly IAttributeRepository _attributeRepository;
		private readonly IMapper _mapper;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		/// <summary>
		/// Initializes a new instance of the <see cref="AttributeController"/> class.
		/// </summary>
		/// <param name="attributeRepository">The attribute repository to retrieve and manage attributes.</param>
		/// <param name="mapper">The AutoMapper instance for object mapping.</param>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations.</param>
		public AttributeController(IAttributeRepository attributeRepository, IMapper mapper, IAuthorizationMiddleware authorizationMiddleware)
		{
			_attributeRepository = attributeRepository;
			_mapper = mapper;
			_authorizationMiddleware = authorizationMiddleware;
		}

		#region Get Methods
		/// <summary>
		/// Get all attributes.
		/// </summary>
		/// <returns>Returns an array of all attributes.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when no attributes are found in the database.</exception>
		[HttpGet]
        [ProducesResponseType(200, Type = typeof(List<AttributeDTO>))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttributes()
		{
			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ATT-1101");

			// Retrieve all attributes from the repository
			var attributes = await _attributeRepository.GetAllAttributes();

			// Check if any attributes were found
			if (attributes.Count == 0)
				throw new NotFoundException("There are no attributes in the database, please try again", "ATT-1401");

			return Ok(attributes);
		}

		/// <summary>
		/// Get an attribute based on the ID.
		/// </summary>
		/// <param name="id">The ID of the attribute.</param>
		/// <returns>Returns the attribute with the specified ID.</returns>
		/// <exception cref="NotFoundException">Thrown when the attribute with the specified ID doesn't exist.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		[HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(AttributeDTO))]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> GetAttribute(int id)
        {
			// Check if the attribute with the given ID exists
			if (!await _attributeRepository.AttributeExists(id))
				throw new NotFoundException("The attribute with ID " + id + " doesn't exist", "ATT-1402");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ATT-1101");

			// Retrieve the attribute by its ID
			var attribute = await _attributeRepository.GetAttributeById(id);
			return Ok(attribute);

		}
		#endregion

		#region Post Methods
		/// <summary>
		/// Create a new attribute.
		/// </summary>
		/// <param name="attribute">The attribute to create.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the provided attribute data is incomplete.</exception>
		[HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> CreateAttribute([FromBody] CreateAttributeDTO attribute)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "ATT-2001");

			// Check if the provided attribute data is complete
			if (attribute == null || attribute.Name == null || attribute.Type == null)
				throw new BadRequestException("Some fields are missing, please try again with complete data", "ATT-1102");

			// Create the attribute using the repository
			await _attributeRepository.CreateAttribute(attribute);
			return StatusCode(201);
		}
		#endregion

		#region Put Methods
		/// <summary>
		/// Update an existing attribute.
		/// </summary>
		/// <param name="id">The ID of the attribute to update.</param>
		/// <param name="attributeUpdate">The updated attribute data.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or the provided attribute update data is incomplete.</exception>
		/// <exception cref="NotFoundException">Thrown when the attribute with the specified ID doesn't exist.</exception>
		[HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> UpdateAttribute(int id, [FromBody] UpdateAttributeDTO attributeUpdate)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "ATT-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ATT-1101");

			// Check if the provided attribute update data is complete
			if (attributeUpdate == null)
				throw new BadRequestException("Some fields are missing, please try again with complete data", "ATT-1102");

			// Check if the attribute with the given ID exists
			if (!await _attributeRepository.AttributeExists(id))
				throw new NotFoundException("The attribute with ID " + id + " doesn't exist", "ATT-1402");

			// Update the attribute using the repository
			await _attributeRepository.UpdateAttribute(id, attributeUpdate);
			return Ok();
		}
		#endregion

		#region Delete Methods
		/// <summary>
		/// Delete an existing attribute.
		/// </summary>
		/// <param name="id">The ID of the attribute to delete.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the attribute with the specified ID doesn't exist.</exception>
		[HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
        [ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
        [ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
        [ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
        [ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
        public async Task<IActionResult> DeleteAttribute(int id)
        {
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "ATT-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "ATT-1101");

			// Check if the attribute with the given ID exists
			if (!await _attributeRepository.AttributeExists(id))
				throw new NotFoundException("The attribute with ID " + id + " doesn't exist", "ATT-1402");

			// Delete the attribute using the repository
			await _attributeRepository.DeleteAttribute(id);
			return Ok();
		}
		#endregion
    }
}
