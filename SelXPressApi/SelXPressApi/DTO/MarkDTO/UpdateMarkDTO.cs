namespace SelXPressApi.DTO.MarkDTO;

/// <summary>
/// DTO to update the Mark. 
/// Here you can access to model <see cref="Models.Mark"/>. 
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
public class UpdateMarkDTO
{
    /// <summary>
    /// Rate of the Mark
    /// </summary>
    public float Rate { get; set; }
}