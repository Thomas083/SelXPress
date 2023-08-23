using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.TagDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// API controller for managing tag.
	/// </summary>
	[Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationMiddleware _authorizationMiddleware;

		/// <summary>
		/// Initializes a new instance of the <see cref="TagController"/> class.
		/// </summary>
		/// <param name="tagRepository">The tag repository to retrieve and manage tags.</param>
		/// <param name="mapper">The AutoMapper instance for object mapping.</param>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations.</param>
		public TagController(ITagRepository tagRepository, IMapper mapper, IAuthorizationMiddleware authorizationMiddleware)
		{
			_tagRepository = tagRepository;
			_mapper = mapper;
			_authorizationMiddleware = authorizationMiddleware;
		}

		#region Get Methods
		/// <summary>
		/// Get all tags.
		/// </summary>
		/// <returns>Returns an array of all tags.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when no tags are found in the database.</exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<TagDto>))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetTags()
		{
			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is invalid, a bad request occurred", "TAG-1101");

			// Retrieve all tags from the repository
			var tags = _mapper.Map<List<TagDto>>(await _tagRepository.GetAllTags());

			// Check if any tags were found
			if (tags.Count == 0)
				throw new NotFoundException("No tags found in the database", "TAG-1401");

			return Ok(tags);
		}

		/// <summary>
		/// Get a tag based on the ID.
		/// </summary>
		/// <param name="id">The ID of the tag.</param>
		/// <returns>Returns the tag with the specified ID.</returns>
		/// <exception cref="NotFoundException">Thrown when the tag with the specified ID doesn't exist.</exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(TagDto))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetTag(int id)
		{
			// Check if the tag with the given ID exists
			if (!await _tagRepository.TagExists(id))
				throw new NotFoundException($"Tag with ID: {id} does not exist", "TAG-1402");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is invalid, a bad request occurred", "TAG-1101");

			// Retrieve the tag by its ID
			var tag = _mapper.Map<TagDto>(await _tagRepository.GetTagById(id));
			return Ok(tag);
		}
		#endregion

		#region Post Methods
		/// <summary>
		/// Create a new tag.
		/// </summary>
		/// <param name="newTag">The tag to create.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the provided tag data is incomplete.</exception>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateTag([FromBody] CreateTagDTO newTag)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "TAG-2001");

			// Check if the provided tag data is complete
			if (newTag.Name == null)
				throw new BadRequestException("Missing fields, please provide valid data", "TAG-1102");

			// Create the tag using the repository
			await _tagRepository.CreateTag(newTag);
			return StatusCode(201);
		}
		#endregion

		#region Put Methods
		/// <summary>
		/// Update an existing tag.
		/// </summary>
		/// <param name="id">The ID of the tag to update.</param>
		/// <param name="tagUpdate">The updated tag data.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or the provided tag update data is incomplete.</exception>
		/// <exception cref="NotFoundException">Thrown when the tag with the specified ID doesn't exist.</exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateTag(int id, [FromBody] UpdateTagDTO tagUpdate)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "TAG-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is invalid, a bad request occurred", "TAG-1101");

			// Check if the provided tag update data is complete
			if (tagUpdate == null)
				throw new BadRequestException("Missing fields, please provide valid data", "TAG-1102");

			// Check if the tag with the given ID exists
			if (!await _tagRepository.TagExists(id))
				throw new NotFoundException($"Tag with ID: {id} does not exist", "TAG-1402");

			// Update the tag using the repository
			await _tagRepository.UpdateTag(id, tagUpdate);
			return Ok();
		}
		#endregion

		#region Delete Methods
		/// <summary>
		/// Delete an existing tag.
		/// </summary>
		/// <param name="id">The ID of the tag to delete.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the tag with the specified ID doesn't exist.</exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteTag(int id)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "TAG-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is invalid, a bad request occurred", "TAG-1101");

			// Check if the tag with the given ID exists
			if (!await _tagRepository.TagExists(id))
				throw new NotFoundException($"Tag with ID: {id} does not exist", "TAG-1402");

			// Delete the tag using the repository
			await _tagRepository.DeleteTag(id);
			return Ok();
		}
		#endregion
	}
}
