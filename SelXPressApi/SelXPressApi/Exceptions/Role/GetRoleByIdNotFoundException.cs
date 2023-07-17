namespace SelXPressApi.Exceptions.Role;

/// <summary>
/// Class for the GetRoleByIdNotFoundException's exception
/// </summary>
public class GetRoleByIdNotFoundException : Exception
{
    /// <summary>
    /// Constructor of the GetRoleByIdNotFoundException's exception
    /// </summary>
    /// <param name="message"></param>
    public GetRoleByIdNotFoundException(string message) : base(message)
    {
        
    }
}