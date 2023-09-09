namespace SelXPressApi.DTO.CommentDTO;

/// <summary>
/// DTO to update the Comment. 
/// Here you can access to model <see cref="Models.Comment"/>. 
/// The main DTO is <see cref="CommentDTO"/>.
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
public class UpdateCommentDTO
{
    /// <summary>
    /// Message of the comment
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// Tile of the comment
    /// </summary>
    public string Title { get; set; }
    
}