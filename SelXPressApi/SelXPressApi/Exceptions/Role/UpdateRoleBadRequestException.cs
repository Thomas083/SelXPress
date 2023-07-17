namespace SelXPressApi.Exceptions.Role;

/// <summary>
/// Class for UpdateRoleBadRequestException's exception
/// </summary>
public class UpdateRoleBadRequestException : Exception
{
    /// <summary>
    /// Constructor for the UpdateRoleBadRequestException's exception
    /// </summary>
    /// <param name="message"></param>
    public UpdateRoleBadRequestException(string message): base(message)
    {
        
    }
}