﻿using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DocumentationErrorTemplate;
using SelXPressApi.DTO.RoleDTO;
using SelXPressApi.Exceptions.Role;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using SelXPressApi.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SelXPressApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IRoleRepository _roleRepository;

		public RoleController(IRoleRepository roleRepository)
		{
			_roleRepository = roleRepository;
		}
		/// <summary>
		/// GET: api/<RoleController>
		/// Get all roles
		/// </summary>
		/// <returns>Return an Array with all roles </returns>
		[HttpGet]
		[ProducesResponseType(200, Type = typeof(List<Role>))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetRoles()
		{
			if (!ModelState.IsValid)
				throw new GetRolesBadRequestException("The model is wrong, a bad request occured");
			var roles = await _roleRepository.GetAllRoles();

			if (roles.Count == 0)
				throw new GetRolesNotFoundException("There is no roles in the database, please try again");
			
			return Ok(roles);
		}

		/// <summary>
		/// GET api/<RoleController>/5
		/// Get a role by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Return a list of user with a specific role</returns>
		[HttpGet("{id}")]
		[ProducesResponseType(200, Type = typeof(Role))]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> GetRole(int id)
		{
			if (!await _roleRepository.RoleExists(id))
				throw new GetRoleByIdNotFoundException("The role with the id : " + id + " doesn't exist");
			if (!ModelState.IsValid)
				throw new GetRoleByIdBadRequestException("The model is wrong, a bad request occured");
			var role = await _roleRepository.GetRoleById(id);
			return Ok(role);
		}

		/// <summary>
		/// POST api/<RoleController>
		/// Create a new role
		/// </summary>
		/// <param name="value"></param>
		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400 , Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> CreateRole([FromBody] CreateRoleDTO role)
		{
			if (role == null || role.RoleName == null)
				throw new CreateRoleBadRequestException("There is missing fields, please try again with some data");
			if (await _roleRepository.CreateRole(role))
			{
				return StatusCode(201);
			}
			throw new Exception("An error occured while the creating of the role");
		}

		/// <summary>
		/// PUT api/<RoleController>/5
		/// Modify a role
		/// </summary>
		/// <param name="id"></param>
		/// <param name="value"></param>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> UpdateRole(int id, [FromBody] UpdateRoleDTO updateRole)
		{
			if (!ModelState.IsValid)
				throw new UpdateRoleBadRequestException("The model is wrong, a bad request occured");
			if (updateRole == null)
				throw new UpdateRoleBadRequestException("There is missing fields, please try again with some data");
			if (!await _roleRepository.RoleExists(id))
				throw new UpdateRoleNotFoundException("The role with the id : " + id + " doesn't exist");
			await _roleRepository.UpdateRoleByID(id, updateRole);
			return Ok();
		}

		/// <summary>
		/// DELETE api/<RoleController>/5
		/// Delete a role
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400, Type = typeof(BadRequestErrorTemplate))]
		[ProducesResponseType(404, Type = typeof(NotFoundErrorTemplate))]
		[ProducesResponseType(500, Type = typeof(InternalServerErrorTemplate))]
		public async Task<IActionResult> Delete(int id)
		{
			if (!await _roleRepository.RoleExists(id))
				throw new DeleteRoleNotFoundException("The role with the id : " + id + " doesn't exist");
			if (!ModelState.IsValid)
				throw new DeleteRoleBadRequestException("The model is wrong, a bad request occured");
			if (await _roleRepository.DeleteRole(id))
			{
				return Ok();
			}
			await _roleRepository.DeleteRole(id);
			return Ok();
		}
	}
}
