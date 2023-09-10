namespace SelXPressApi.DocumentationErrorTemplate;

/// <summary>
/// Class use for return an error with the HTTP status at 403
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
public class ForbiddenErrorTemplate
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
    public int Status { get; set; }
    
}