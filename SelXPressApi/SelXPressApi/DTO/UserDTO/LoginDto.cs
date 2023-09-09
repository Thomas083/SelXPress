using System.ComponentModel.DataAnnotations;

namespace SelXPressApi.DTO.UserDTO;

/// <summary>
/// Dto for the login of the User. 
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
public class LoginDto
{
    /// <summary>
    /// Email of the user
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Password of the user
    /// </summary>
    public string Password { get; set; }
    
}