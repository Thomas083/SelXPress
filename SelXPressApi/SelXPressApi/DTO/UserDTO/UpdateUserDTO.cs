namespace SelXPressApi.DTO.UserDTO
{
	/// <summary>
	/// DTO to update User. 
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
	public class UpdateUserDTO
    {
        /// <summary>
        /// The new Username of the user
        /// </summary>
        public string Username { get; set; }
        
        
    }
}
