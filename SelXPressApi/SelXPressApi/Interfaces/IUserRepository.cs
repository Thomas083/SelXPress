using Microsoft.AspNetCore.Mvc;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SelXPressApi.Interfaces
{
	/// <summary>
	/// Interface for performing CRUD operations on users.
	/// </summary>
	public interface IUserRepository
	{
		/// <summary>
		/// Retrieves a list of all users including their roles.
		/// </summary>
		/// <returns>A list of users with associated roles.</returns>
		Task<List<User>> GetAllUsers();

		/// <summary>
		/// Retrieves a user by their ID including their role.
		/// </summary>
		/// <param name="id">The ID of the user to retrieve.</param>
		/// <returns>The retrieved user with associated role, or null if not found.</returns>
		Task<User?> GetUserById(int id);

		/// <summary>
		/// Checks if a user with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the user to check.</param>
		/// <returns>True if the user exists; otherwise, false.</returns>
		Task<bool> UserExists(int id);

		/// <summary>
		/// Creates a new user.
		/// </summary>
		/// <param name="createUser">DTO containing user details.</param>
		/// <returns>True if the user was created successfully; otherwise, false.</returns>
		Task<bool> CreateUser(CreateUserDto createUser);

		/// <summary>
		/// Updates a user's information.
		/// </summary>
		/// <param name="updateUser">DTO containing updated user details.</param>
		/// <param name="email">The email of the user to update.</param>
		/// <returns>True if the user was updated successfully; otherwise, false.</returns>
		Task<bool> UpdateUser(UpdateUserDTO updateUser, string email);

		/// <summary>
		/// Retrieves a user by their email.
		/// </summary>
		/// <param name="email">The email of the user to retrieve.</param>
		/// <returns>The retrieved user with associated role, or null if not found.</returns>
		Task<User> GetUserByEmail(string email);

		/// <summary>
		/// Checks if a user with the given email exists.
		/// </summary>
		/// <param name="email">The email of the user to check.</param>
		/// <returns>True if the user exists; otherwise, false.</returns>
		Task<bool> UserExistsEmail(string email);

		/// <summary>
		/// Deletes a user by their ID.
		/// </summary>
		/// <param name="id">The ID of the user to delete.</param>
		/// <returns>True if the user was deleted successfully; otherwise, false.</returns>
		Task<bool> DeleteUser(int id);
	}
}
