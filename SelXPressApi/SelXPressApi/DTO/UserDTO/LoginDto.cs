using System.ComponentModel.DataAnnotations;

namespace SelXPressApi.DTO.UserDTO;

/// <summary>
/// Dto for the login of the user
/// </summary>
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