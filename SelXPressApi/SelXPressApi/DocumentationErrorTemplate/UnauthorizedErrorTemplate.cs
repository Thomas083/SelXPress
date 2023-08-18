namespace SelXPressApi.DocumentationErrorTemplate;

/// <summary>
/// Class use for return an error with the HTTP status at 401
/// </summary>
public class UnauthorizedErrorTemplate
{
    /// <summary>
    /// Message of the exception
    /// </summary>
    public string Message { get; set; }
    
    /// <summary>
    /// Error code of the exception
    /// </summary>
    public string Code { get; set; }
    
    /// <summary>
    /// Status of the HTTP status
    /// </summary>
    public string Status { get; set; }
}