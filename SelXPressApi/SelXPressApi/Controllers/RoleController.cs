using System.Net;
using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.RoleDTO;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Middleware;
using SelXPressApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IRoleRepository _roleRepository;
		private readonly IAuthorizationMiddleware _authorizationMiddleware;

		public RoleController(IRoleRepository roleRepository, IAuthorizationMiddleware authorizationMiddleware)
		{
			_roleRepository = roleRepository;
			_authorizationMiddleware = authorizationMiddleware;
		}
		
		/// <summary>
		/// Method to get all the roles of the database
		/// </summary>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Role>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetRoles()
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "RLE-2001");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "RLE-1101");
			
			var roles = await _roleRepository.GetAllRoles();

			if (roles.Count == 0)
				throw new NotFoundException("There is no roles in the database, please try again", "RLE-1401");
			
			return Ok(roles);
		}

		/// <summary>
		/// Return the role based on the parameter id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Role))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetRole(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);

			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "RLE-2001");
			
			if (!await _roleRepository.RoleExists(id))
				throw new NotFoundException("The role with the id : " + id + " doesn't exist", "RLE-1402");
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "RLE-1101");
			var role = await _roleRepository.GetRoleById(id);
			return Ok(role);
		}

		/// <summary>
		/// Method to create a role
		/// </summary>
		/// <param name="role"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="Exception"></exception>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400 , Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO role)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "RLE-2001");
			
			if (role == null || role.RoleName == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "RLE-1102");
			
			await _roleRepository.CreateRole(role);
			return StatusCode(201);
		}

		/// <summary>
		/// Method to update the role based on an id
		/// </summary>
		/// <param name="id"></param>
		/// <param name="updateRole"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="BadRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDTO updateRole)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "RLE-2001");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured", "RLE-1101");
			if (updateRole == null)
				throw new BadRequestException("There are missing fields, please try again with some data", "RLE-1102");
			if (!await _roleRepository.RoleExists(id))
				throw new NotFoundException("The role with the id : " + id + " doesn't exist", "RLE-1402");
			await _roleRepository.UpdateRoleByID(id, updateRole);
			return Ok();
		}

		/// <summary>
		/// Method to delete a role
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		/// <exception cref="ForbiddenRequestException"></exception>
		/// <exception cref="NotFoundException"></exception>
		/// <exception cref="BadRequestException"></exception>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(401, Type = typeof(UnauthorizedErrorTemplate))]
		[ProducesResponseType(403, Type = typeof(ForbiddenErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> DeleteRole(int id)
		{
			await _authorizationMiddleware.CheckIfTokenExists(HttpContext);
			
			if (!await _authorizationMiddleware.CheckRoleIfAdmin(HttpContext))
				throw new ForbiddenRequestException("You are not authorized to do this operation", "RLE-2001");
			
			if (!await _roleRepository.RoleExists(id))
				throw new NotFoundException("The role with the id : " + id + " doesn't exist", "RLE-1402");
			
			if (!ModelState.IsValid)
				throw new BadRequestException("The model is wrong, a bad request occured","RLE-1101");
			
			await _roleRepository.DeleteRole(id);
			return Ok();
		}
	}
}
