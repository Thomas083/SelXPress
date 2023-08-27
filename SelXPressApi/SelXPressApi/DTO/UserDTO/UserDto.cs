using SelXPressApi.Models;

namespace SelXPressApi.DTO.UserDTO
{
	/// <summary>
	/// DTO representing user details.
	/// </summary>
	public class UserDto
	{
		/// <summary>
		/// Gets or sets the ID of the user.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// Gets or sets the username of the user.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the email of the user.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the role of the user.
		/// </summary>
		public Role Role { get; set; }
	}
}
