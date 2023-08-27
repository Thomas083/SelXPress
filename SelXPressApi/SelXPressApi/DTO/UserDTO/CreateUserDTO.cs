namespace SelXPressApi.DTO.UserDTO
{
	/// <summary>
	/// DTO for creating a new user.
	/// </summary>
	public class CreateUserDto
	{
		/// <summary>
		/// Gets or sets the username of the new user.
		/// </summary>
		public string Username { get; set; }

		/// <summary>
		/// Gets or sets the password of the new user.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the email of the new user.
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		/// Gets or sets the ID of the role for the new user.
		/// </summary>
		public int RoleId { get; set; }
	}
}
