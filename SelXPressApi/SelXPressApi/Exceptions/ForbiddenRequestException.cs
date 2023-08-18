namespace SelXPressApi.Exceptions;

/// <summary>
/// 
/// </summary>
public class ForbiddenRequestException : CommonException
{
    public ForbiddenRequestException(string message,string code) : base(message, code)
    {
        
    }
}