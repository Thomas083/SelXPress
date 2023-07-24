using SelXPressApi.Models;

namespace SelXPressApi.DTO.CommentDTO;

/// <summary>
/// DTO to get the comment
/// </summary>
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