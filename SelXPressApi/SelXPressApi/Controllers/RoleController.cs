using System.Net;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.RoleDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Models;

namespace SelXPressApi.Controllers
{
	/// <summary>
	/// API controller for managing roles.
	/// </summary>
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IRoleRepository _roleRepository;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		/// <summary>
		/// Initializes a new instance of the <see cref="RoleController"/> class.
		/// </summary>
		/// <param name="roleRepository">The role repository to retrieve and manage roles.</param>
		/// <param name="authorizationMiddleware">The middleware for authorization-related operations.</param>
		public RoleController(IRoleRepository roleRepository, IAuthorizationMiddleware authorizationMiddleware)
		{
			_roleRepository = roleRepository;
			_authorizationMiddleware = authorizationMiddleware;
		}

		#region Get Methods
		/// <summary>
		/// Get all roles.
		/// </summary>
		/// <returns>Returns an array of all roles.</returns>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="UnauthorizedErrorTemplate">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="ForbiddenErrorTemplate">Thrown when the user is authorized but doesn't have the required permissions.</exception>
		/// <exception cref="NotFoundErrorTemplate">Thrown when no roles are found in the database.</exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Role>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetRoles()
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "RLE-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "RLE-1101");

			// Retrieve all roles from the repository
			var roles = await _roleRepository.GetAllRoles();

			if (roles.Count == 0)
				throw new NotFoundException("There are no roles in the database, please try again", "RLE-1401");

			return Ok(roles);
		}
		#endregion

		#region Post Methods
		/// <summary>
		/// Create a new role.
		/// </summary>
		/// <param name="role">The role to create.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the provided role data is incomplete.</exception>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO role)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "RLE-2001");

			// Check if the provided role data is complete
			if (role == null || role.RoleName == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "RLE-1102");

			// Create the role using the repository
			await _roleRepository.CreateRole(role);
			return StatusCode(201);
		}
		#endregion

		#region Put Methods
		/// <summary>
		/// Update an existing role.
		/// </summary>
		/// <param name="id">The ID of the role to update.</param>
		/// <param name="updateRole">The updated role data.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid or the provided role update data is incomplete.</exception>
		/// <exception cref="NotFoundException">Thrown when the role with the specified ID doesn't exist.</exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDTO updateRole)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "RLE-2001");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "RLE-1101");

			// Check if the provided role update data is complete
			if (updateRole == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "RLE-1102");

			// Check if the role with the given ID exists
			if (!await _roleRepository.RoleExists(id))
				throw new NotFoundException("The role with the id : " + id + " doesn't exist", "RLE-1402");

			// Update the role using the repository
			await _roleRepository.UpdateRoleByID(id, updateRole);
			return Ok();
		}
		#endregion

		#region Delete Methods
		/// <summary>
		/// Delete an existing role.
		/// </summary>
		/// <param name="id">The ID of the role to delete.</param>
		/// <returns>Returns a status code indicating the result of the operation.</returns>
		/// <exception cref="ForbiddenRequestException">Thrown when the user is not authorized to perform this operation.</exception>
		/// <exception cref="BadRequestException">Thrown when the model state is invalid.</exception>
		/// <exception cref="NotFoundException">Thrown when the role with the specified ID doesn't exist.</exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteRole(int id)
		{
			// Check if a valid token exists in the HttpContext
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			// Check if the user has admin role
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "RLE-2001");

			// Check if the role with the given ID exists
			if (!await _roleRepository.RoleExists(id))
				throw new NotFoundException("The role with the id : " + id + " doesn't exist", "RLE-1402");

			// Check if the model state is valid
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occurred", "RLE-1101");

			// Delete the role using the repository
			await _roleRepository.DeleteRole(id);
			return Ok();
		}
		#endregion
	}
}
