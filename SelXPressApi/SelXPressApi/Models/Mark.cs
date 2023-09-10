namespace SelXPressApi.Models;


/// <summary>
/// Model of the Mark table. 
/// <see cref="DTO.MarkDTO"/>
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
public class Mark
{
    /// <summary>
    /// Id of mark of the comment
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// value of the mark of the comment
    /// </summary>
    public float rate { get; set; }
}