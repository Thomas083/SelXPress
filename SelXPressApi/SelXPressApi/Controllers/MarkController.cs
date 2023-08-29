using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.MarkDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// API controller for managing marks.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class MarkController : ControllerBase
	{
		private readonly IMarkRepository _markRepository;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;
		/// <summary>
		/// Initializes a new instance of the <see cref="MarkController"/> class.
		/// </summary>
		/// <param name="markRepository">The repository for managing marks.</param>
		/// <param name="authorizationMiddleware">Middleware for authorization checks.</param>
		public MarkController(IMarkRepository markRepository, IAuthorizationMiddleware authorizationMiddleware)
		{
			_markRepository = markRepository ?? throw new ArgumentNullException(nameof(markRepository));
			_authorizationMiddleware = authorizationMiddleware ?? throw new ArgumentNullException(nameof(authorizationMiddleware));
		}

		#region Get Methods
		/// <summary>
		/// Retrieves a list of all marks from the database.
		/// </summary>
		/// <returns>Returns a list of marks if successful.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown if the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown if the request model is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown if there are no marks in the database.</exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Mark>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetAllMarks()
		{
			// Check if a valid token exists in the HttpContext.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role.
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "MRK-2001");

			// Check if the model state is valid.
			if (!ModelState.IsValid)
				throw new BadRequestException("The request model is invalid", "MRK-1101");

			// Retrieve all marks from the repository.
			var marks = await _markRepository.GetAllMark();

			// If no marks are found, throw a NotFoundException.
			if (marks.Count == 0)
				throw new NotFoundException("No marks found in the database, please try again", "MRK-1401");

			// Return the list of marks.
			return Ok(marks);
		}

		/// <summary>
		/// Retrieves a specific mark by its ID from the database.
		/// </summary>
		/// <param name="id">The ID of the mark to retrieve.</param>
		/// <returns>Returns the requested mark if successful.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown if the user is not authorized to perform this operation.</exception>
		/// <exception cref="NotFoundException">Thrown if the mark with the specified ID doesn't exist in the database.</exception>
		/// <exception cref="BadRequestException">Thrown if the request model is invalid.</exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Mark))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetMarkById(int id)
		{
			// Check if a valid token exists in the HttpContext.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role.
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "MRK-2001");

			// Check if the mark with the specified ID exists.
			if (!await _markRepository.MarkExists(id))
				throw new NotFoundException("The mark with the ID " + id + " doesn't exist", "MRK-1002");

			// Check if the model state is valid.
			if (!ModelState.IsValid)
				throw new BadRequestException("The request model is invalid", "MRK-1402");

			// Retrieve the mark with the specified ID from the repository.
			var mark = await _markRepository.GetMarkById(id);

			// Return the requested mark.
			return Ok(mark);
		}

		/// <summary>
		/// Retrieves a list of marks associated with a specific user from the database.
		/// </summary>
		/// <param name="id">The ID of the user for whom to retrieve marks.</param>
		/// <returns>Returns a list of marks associated with the specified user if successful.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown if the user is not authorized to perform this operation.</exception>
		/// <exception cref="NotFoundException">Thrown if there are no marks associated with the specified user.</exception>
		/// <exception cref="BadRequestException">Thrown if the request model is invalid.</exception>
		[HttpGet("{id}/user")]
		[ProducesResponseType(200, Type = typeof(List<Mark>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetMarkByUser(int id)
		{
			// Check if a valid token exists in the HttpContext.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role.
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "MRK-2001");

			// Retrieve marks associated with the user with the specified ID from the repository.
			var marks = await _markRepository.GetMarkByUser(id);

			// Check if there are no marks associated with the specified user.
			if (marks.Count == 0)
				throw new NotFoundException("There are no marks for the user with the ID : " + id, "MRK-1403");

			// Check if the model state is valid.
			if (!ModelState.IsValid)
				throw new BadRequestException("The request model is invalid", "MRK-1101");

			// Return the list of marks associated with the specified user.
			return Ok(marks);
		}


		/// <summary>
		/// Retrieves a list of marks associated with a specific product from the database.
		/// </summary>
		/// <param name="id">The ID of the product for which to retrieve marks.</param>
		/// <returns>Returns a list of marks associated with the specified product if successful.</returns>
		/// <exception cref="NotFoundException">Thrown if there are no marks associated with the specified product.</exception>
		/// <exception cref="BadRequestException">Thrown if the request model is invalid.</exception>
		[HttpGet("{id}/product")]
		[ProducesResponseType(200, Type = typeof(List<Mark>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetMarkByProduct(int id)
		{
			// Retrieve marks associated with the product with the specified ID from the repository.
			var marks = await _markRepository.GetMarkByProduct(id);

			// Check if there are no marks associated with the specified product.
			if (marks.Count == 0)
				throw new NotFoundException("There are no marks for the product with the ID : " + id, "MRK-1404");

			// Check if the model state is valid.
			if (!ModelState.IsValid)
				throw new BadRequestException("The request model is invalid", "MRK-1101");

			// Return the list of marks associated with the specified product.
			return Ok(marks);
		}
		#endregion

		#region Post Methods
		/// <summary>
		/// Creates a new mark in the database based on the provided data.
		/// </summary>
		/// <param name="markDto">The data required to create a new mark.</param>
		/// <returns>Returns an OK result if the mark creation is successful.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown if the user is not authorized to perform the operation.</exception>
		/// <exception cref="BadRequestException">Thrown if the request model is invalid or missing fields.</exception>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateMark([FromBody] CreateMarkDTO markDto)
		{
			// Check if a token exists in the current HTTP context.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has the necessary role to perform the operation.
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext) &&
				!await _authorizationMiddleware.CheckRoleIfCustomer(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "MRK-2001");

			// Check if the provided mark data is missing.
			if (markDto == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "MRK-1102");

			// Check if the model state is valid.
			if (!ModelState.IsValid)
				throw new BadRequestException("The request model is invalid", "MRK-1101");

			// Create a new mark based on the provided data.
			await _markRepository.CreateMark(markDto);

			// Return an OK result indicating successful mark creation.
			return StatusCode(201);
		}
		#endregion

		#region Put Methods
		/// <summary>
		/// Updates the details of an existing mark in the database based on the provided data.
		/// </summary>
		/// <param name="id">The ID of the mark to be updated.</param>
		/// <param name="updateMarkDto">The updated data for the mark.</param>
		/// <returns>Returns an OK result if the mark update is successful.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown if the user is not authorized to perform the operation.</exception>
		/// <exception cref="BadRequestException">Thrown if the request model is invalid or missing fields.</exception>
		/// <exception cref="NotFoundException">Thrown if the mark with the specified ID does not exist.</exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateMark(int id, [FromBody] UpdateMarkDTO updateMarkDto)
		{
			// Check if a token exists in the current HTTP context.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has the necessary role to perform the operation.
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "MRK-2001");

			// Check if the provided updated mark data is missing.
			if (updateMarkDto == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "MRK-1102");

			// Check if the model state is valid.
			if (!ModelState.IsValid)
				throw new BadRequestException("The request model is invalid", "MRK-1101");

			// Check if the mark with the specified ID exists.
			if (!await _markRepository.MarkExists(id))
				throw new NotFoundException("The mark with the ID: " + id + " doesn't exist", "MRK-1402");

			// Update the mark's details using the provided data and ID.
			await _markRepository.UpdateMarkById(updateMarkDto, id);

			// Return an OK result indicating successful mark update.
			return Ok();
		}
		#endregion

		#region Delete Methods
		/// <summary>
		/// Deletes a mark from the database based on the provided ID.
		/// </summary>
		/// <param name="id">The ID of the mark to be deleted.</param>
		/// <returns>Returns an OK result if the mark deletion is successful.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown if the user is not authorized to perform the operation.</exception>
		/// <exception cref="NotFoundException">Thrown if the mark with the specified ID does not exist.</exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteMark(int id)
		{
			// Check if a token exists in the current HTTP context.
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has the necessary role to perform the operation.
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to perform this operation", "MRK-2001");

			// Check if the mark with the specified ID exists.
			if (!await _markRepository.MarkExists(id))
				throw new NotFoundException("The mark with the ID: " + id + " doesn't exist", "MRK-1402");

			// Check if the model state is valid.
			if (!ModelState.IsValid)
				throw new BadRequestException("The request model is invalid", "MRK-1101");

			// Delete the mark from the database using the provided ID.
			await _markRepository.DeleteMarkById(id);

			// Return an OK result indicating successful mark deletion.
			return Ok();
		}
		#endregion
	}
}
