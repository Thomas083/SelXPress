namespace SelXPressApi.Exceptions.Role;

/// <summary>
/// Class for the GetRolesBadRequestException's exception
/// </summary>
public class GetRolesBadRequestException : Exception
{
    /// <summary>
    /// Constructor of the GetRolesBadRequestException's exception
    /// </summary>
    /// <param name="message"></param>
    public GetRolesBadRequestException(string message) : base(message)
    {
        
    }
}