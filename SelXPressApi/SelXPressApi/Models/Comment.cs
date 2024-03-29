namespace SelXPressApi.Models;

/// <summary>
/// Model of the Comment table. 
/// <see cref="DTO.CommentDTO"/>
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
public class Comment
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
    /// Date and time of the creation of the comment
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
    /// <summary>
    /// Title of the comment
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// User object of the user's comment
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Product object of the product's comment
    /// </summary>
    public Product Product { get; set; }

    /// <summary>
    /// Mark of the comment
    /// </summary>
    public Mark Mark { get; set; } // unique
}