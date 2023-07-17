namespace SelXPressApi.Exceptions.Role;

/// <summary>
/// Class for the CreateRoleBadRequestException's exception
/// </summary>
public class CreateRoleBadRequestException : Exception
{
    /// <summary>
    /// Constructor for the CreateRoleBadRequestException's exception
    /// </summary>
    /// <param name="message"></param>
    public CreateRoleBadRequestException(string message) : base(message)
    {
        
    }
}