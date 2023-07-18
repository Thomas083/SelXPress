namespace SelXPressApi.Exceptions;

public class UnauthorizedException : CommonException
{
    public UnauthorizedException(string message, string code) : base(message,code)
    {
    }
}