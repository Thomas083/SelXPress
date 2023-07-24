namespace SelXPressApi.DTO.CommentDTO;

/// <summary>
/// DTO to create a comment
/// </summary>
public class CreateCommentDTO
{
    /// <summary>
    /// Message of the comment
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// Date of the comment
    /// </summary>
    public DateTime CreatedAt { get; set; }
    
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