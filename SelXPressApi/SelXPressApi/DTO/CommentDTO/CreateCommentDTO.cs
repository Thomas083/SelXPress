namespace SelXPressApi.DTO.CommentDTO;

/// <summary>
/// DTO to create a Comment. 
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
public class CreateCommentDTO
{
    /// <summary>
    /// Message of the comment
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// Title of the comment
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Id of the user
    /// </summary>
    public int UserId { get; set; }
    
    /// <summary>
    /// Id of the product
    /// </summary>
    public int ProductId { get; set; }
    
    /// <summary>
    /// Rate of the object Mark
    /// </summary>
    public float Rate { get; set; }
}