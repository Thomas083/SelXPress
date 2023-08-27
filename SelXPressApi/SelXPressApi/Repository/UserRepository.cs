using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.DTO.UserDTO;
using SelXPressApi.Helper;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SelXPressApi.Repository
{
	/// <summary>
	/// Repository for managing users.
	/// </summary>
	public class UserRepository : IUserRepository
	{
		private readonly DataContext _context;
		private readonly ICommonMethods _commonMethods;

		/// <summary>
		/// Initializes a new instance of the <see cref="UserRepository"/> class.
		/// </summary>
		/// <param name="context">The database context.</param>
		/// <param name="commonMethods">Common methods provider.</param>
		public UserRepository(DataContext context, ICommonMethods commonMethods)
		{
			_context = context;
			_commonMethods = commonMethods;
		}

		/// <summary>
		/// Creates a new user.
		/// </summary>
		/// <param name="user">DTO containing user details.</param>
		/// <returns>True if the user was created successfully; otherwise, false.</returns>
		public async Task<bool> CreateUser(CreateUserDto user)
		{
			var role = _context.Roles.Where(r => r.Id == user.RoleId).FirstOrDefault();
			if (role != null)
			{
				User newUser = new User
				{
					Username = user.Username,
					Email = user.Email,
					Role = role
				};
				await _context.Users.AddAsync(newUser);
				return await _commonMethods.Save();
			}
			return false;
		}

		/// <summary>
		/// Retrieves a user by their email.
		/// </summary>
		/// <param name="email">The email of the user to retrieve.</param>
		/// <returns>The retrieved user with associated role, or null if not found.</returns>
		public async Task<User> GetUserByEmail(string email)
		{
			return await _context.Users
				.Where(u => u.Email == email)
				.Join(_context.Roles, user => user.Role.Id, role => role.Id, (user, role) => new User
				{
					Id = user.Id,
					Username = user.Username,
					Email = user.Email,
					Role = role,
				}).FirstAsync();
		}

		/// <summary>
		/// Checks if a user with the given email exists.
		/// </summary>
		/// <param name="email">The email of the user to check.</param>
		/// <returns>True if the user exists; otherwise, false.</returns>
		public async Task<bool> UserExistsEmail(string email)
		{
			return await _context.Users.Where(u => u.Email == email).AnyAsync();
		}

		/// <summary>
		/// Deletes a user by their ID.
		/// </summary>
		/// <param name="id">The ID of the user to delete.</param>
		/// <returns>True if the user was deleted successfully; otherwise, false.</returns>
		public async Task<bool> DeleteUser(int id)
		{
			if (await UserExists(id))
			{
				await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
				return await _commonMethods.Save();
			}
			return false;
		}

		/// <summary>
		/// Retrieves a list of all users including their roles.
		/// </summary>
		/// <returns>A list of users with associated roles.</returns>
		public async Task<List<User>> GetAllUsers()
		{
			return await _context.Users
				.Join(_context.Roles, user => user.Role.Id, role => role.Id, (user, role) => new User
				{
					Id = user.Id,
					Username = user.Username,
					Email = user.Email,
					Role = role,
				}).ToListAsync();
		}

		/// <summary>
		/// Retrieves a user by their ID including their role.
		/// </summary>
		/// <param name="id">The ID of the user to retrieve.</param>
		/// <returns>The retrieved user with associated role, or null if not found.</returns>
		public async Task<User?> GetUserById(int id)
		{
			return await _context.Users
				.Where(u => u.Id == id)
				.Join(_context.Roles, user => user.Role.Id, role => role.Id, (user, role) => new User
				{
					Id = user.Id,
					Username = user.Username,
					Email = user.Email,
					Role = role,
				}).FirstOrDefaultAsync();
		}

		/// <summary>
		/// Updates a user's information.
		/// </summary>
		/// <param name="updateUser">DTO containing updated user details.</param>
		/// <param name="email">The email of the user to update.</param>
		/// <returns>True if the user was updated successfully; otherwise, false.</returns>
		public async Task<bool> UpdateUser(UpdateUserDTO updateUser, string email)
		{
			if (!await UserExistsEmail(email))
				return false;
			User? user = _context.Users.Where(u => u.Email == email).FirstOrDefault();

			if (user != null && updateUser.Username != null && user.Username != updateUser.Username)
				await _context.Users.Where(u => u.Email == email).ExecuteUpdateAsync(p1 => p1.SetProperty(x => x.Username, x => updateUser.Username));
            
			return await _commonMethods.Save();
		}

		/// <summary>
		/// Checks if a user with the specified ID exists.
		/// </summary>
		/// <param name="id">The ID of the user to check.</param>
		/// <returns>True if the user exists; otherwise, false.</returns>
		public async Task<bool> UserExists(int id)
		{
			return await _context.Users.AnyAsync(r => r.Id == id);
		}
	}
}
