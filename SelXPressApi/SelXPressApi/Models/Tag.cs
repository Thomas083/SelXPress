namespace SelXPressApi.Models;

/// <summary>
/// Model of the Tag table. 
/// <see cref="DTO.TagDTO"/>
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
public class Tag
{
    /// <summary>
    /// Id of the tag
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the tag
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Category of the Tag
    /// </summary>
    public Category Category { get; set; }
}