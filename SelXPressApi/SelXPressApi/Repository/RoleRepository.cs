using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.RoleDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelXPressApi.Repository
{
	/// <summary>
	/// Repository for managing roles.
	/// </summary>
	public class RoleRepository : IRoleRepository
	{
		private readonly DataContext _context;
		private ICommonMethods _commonMethods;

		/// <summary>
		/// Initializes a new instance of the <see cref="RoleRepository"/> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		/// <param name="commonMethods">Common methods provider.</param>
		public RoleRepository(DataContext context, ICommonMethods commonMethods)
		{
			_context = context;
			_commonMethods = commonMethods;
		}

		/// <summary>
		/// Creates a new role.
		/// </summary>
		/// <param name="role">The role details.</param>
		/// <returns>True if the role was created successfully; otherwise, false.</returns>
		public async Task<bool> CreateRole(CreateRoleDTO role)
		{
			Role newRole = new Role
			{
				Name = role.RoleName
			};
			await _context.Roles.AddAsync(newRole);
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Deletes a role by its ID.
		/// </summary>
		/// <param name="id">The ID of the role to delete.</param>
		/// <returns>True if the role was deleted successfully; otherwise, false.</returns>
		public async Task<bool> DeleteRole(int id)
		{
			if (await RoleExists(id))
			{
				await _context.Roles.Where(r => r.Id == id).ExecuteDeleteAsync();
				return await _commonMethods.Save();
			}
			return false;
		}

		/// <summary>
		/// Retrieves a list of all roles.
		/// </summary>
		/// <returns>A list of roles.</returns>
		public async Task<List<Role>> GetAllRoles()
		{
			return await _context.Roles.OrderBy(r => r.Id).ToListAsync();
		}

		/// <summary>
		/// Retrieves a role by its ID.
		/// </summary>
		/// <param name="id">The ID of the role to retrieve.</param>
		/// <returns>The retrieved role.</returns>
		public async Task<Role> GetRoleById(int id)
		{
			return await _context.Roles.Where(r => r.Id == id).FirstAsync();
		}

		/// <summary>
		/// Updates a role's information by its ID.
		/// </summary>
		/// <param name="id">The ID of the role to update.</param>
		/// <param name="updateRole">The updated role details.</param>
		/// <returns>True if the role was updated successfully; otherwise, false.</returns>
		public async Task<bool> UpdateRoleByID(int id, UpdateRoleDTO updateRole)
		{
			if (!await RoleExists(id))
				return false;
			Role role = await _context.Roles.Where(r => r.Id == id).FirstAsync();

			if (role != null && updateRole.RoleName != null)
				await _context.Roles.Where(r => r.Id == id)
					.ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Name, x => updateRole.RoleName));
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Checks if a role with the given ID exists.
		/// </summary>
		/// <param name="id">The ID of the role to check.</param>
		/// <returns>True if the role exists; otherwise, false.</returns>
		public async Task<bool> RoleExists(int id)
		{
			return await _context.Roles.Where(r => r.Id == id).AnyAsync();
		}
	}
}
