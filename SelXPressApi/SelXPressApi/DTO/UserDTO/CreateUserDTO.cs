namespace SelXPressApi.DTO.UserDTO
{
	/// <summary>
	/// DTO for creating a new User. 
	/// Here you can access to model <see cref="Models.User"/>. 
	/// The main DTO is <see cref="UserDto"/>.
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
