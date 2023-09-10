namespace SelXPressApi.Models;

/// <summary>
/// Model of the Role table. 
/// <see cref="DTO.RoleDTO"/>
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
public class Role
{
    /// <summary>
    /// Id of the role
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// name of the role
    /// </summary>
    public string Name { get; set; }
}