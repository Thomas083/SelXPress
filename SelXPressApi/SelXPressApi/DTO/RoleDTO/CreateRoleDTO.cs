namespace SelXPressApi.DTO.RoleDTO;

/// <summary>
/// Create Role DTO. 
/// Here you can access to model <see cref="Models.Role"/>. 
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
public class CreateRoleDTO
{
    /// <summary>
    /// Name of the new role
    /// </summary>
    public string RoleName { get; set; }
}