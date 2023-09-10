using SelXPressApi.DTO.RoleDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	/// <summary>
	/// Interface for managing roles.
	/// </summary>
	/// <seealso  cref="Models"/>
	/// <seealso  cref="DTO"/>
	/// <seealso  cref="Controllers"/>
	/// <seealso  cref="Repository"/>
	/// <seealso  cref="Helper"/>
	/// <seealso  cref="DocumentationErrorTemplate"/>
	/// <seealso  cref="Exceptions"/>
	/// <seealso  cref="Interfaces"/>
	/// <seealso  cref="Middleware"/>
	/// <seealso  cref="Data"/>
	public interface IRoleRepository
	{
		/// <summary>
		/// Retrieves a list of all roles.
		/// </summary>
		/// <returns>A list of all roles.</returns>
		Task<List<Role>> GetAllRoles();

		/// <summary>
		/// Retrieves a role by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the role.</param>
		/// <returns>The role with the specified ID.</returns>
		Task<Role> GetRoleById(int id);

		/// <summary>
		/// Updates a role's information by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the role to update.</param>
		/// <param name="updateRole">DTO containing updated role details.</param>
		/// <returns>True if the role was updated successfully; otherwise, false.</returns>
		Task<bool> UpdateRoleByID(int id, UpdateRoleDTO updateRole);

		/// <summary>
		/// Deletes a role by its unique identifier.
		/// </summary>
		/// <param name="id">The unique identifier of the role to delete.</param>
		/// <returns>True if the role was deleted successfully; otherwise, false.</returns>
		Task<bool> DeleteRole(int id);

		/// <summary>
		/// Creates a new role.
		/// </summary>
		/// <param name="role">DTO containing role details.</param>
		/// <returns>True if the role was created successfully; otherwise, false.</returns>
		Task<bool> CreateRole(CreateRoleDTO role);

		/// <summary>
		/// Checks if a role with the given ID exists.
		/// </summary>
		/// <param name="id">The unique identifier of the role to check.</param>
		/// <returns>True if the role exists; otherwise, false.</returns>
		Task<bool> RoleExists(int id);
	}
}
