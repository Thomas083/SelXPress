namespace SelXPressApi.DTO.CommentDTO;

/// <summary>
/// DTO to update the comment
/// </summary>
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