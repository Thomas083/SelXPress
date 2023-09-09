using SelXPressApi.Models;

namespace SelXPressApi.DTO.CommentDTO;

/// <summary>
/// DTO to get the Comment. 
/// Here you can access to model <see cref="Models.Comment"/>. 
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
public class CommentDTO
{
    /// <summary>
    /// Id of the comment
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Message of the comment
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// Title of the comment
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Date and time of the creation of the comment
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// User who create the comment
    /// </summary>
    public User User { get; set; }
    
    /// <summary>
    /// Mark of the comment
    /// </summary>
    public Mark Mark { get; set; }
}