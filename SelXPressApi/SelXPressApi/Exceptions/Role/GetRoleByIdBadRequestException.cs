namespace SelXPressApi.Exceptions.Role;

/// <summary>
/// Class for the GetRoleByIdBadRequestException's exception
/// </summary>
public class GetRoleByIdBadRequestException : Exception
{
    /// <summary>
    /// Constructor of the GetRoleByIdBadRequestException's exception
    /// </summary>
    /// <param name="message"></param>
    public GetRoleByIdBadRequestException(string message) : base(message)
    {
        
    }
}