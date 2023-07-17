namespace SelXPressApi.Exceptions.Role;

/// <summary>
/// Class of the GetRolesNotFoundException's exception
/// </summary>
public class GetRolesNotFoundException : Exception
{
    /// <summary>
    /// Constructor of the GetRolesNotFoundException's exception 
    /// </summary>
    /// <param name="message"></param>
    public GetRolesNotFoundException(string message): base(message)
    {
        
    }
}