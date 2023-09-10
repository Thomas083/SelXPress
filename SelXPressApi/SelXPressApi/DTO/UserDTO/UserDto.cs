using SelXPressApi.Models;

namespace SelXPressApi.DTO.UserDTO
{
	/// <summary>
	/// DTO representing User details. 
	/// Here you can access to model <see cref="Models.User"/>. 
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
	/// <seealso  cref="Data"/>>
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
