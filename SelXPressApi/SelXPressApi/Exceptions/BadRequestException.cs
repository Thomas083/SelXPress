namespace SelXPressApi.Exceptions;

public class BadRequestException : CommonException
{
    public BadRequestException(string message, string code) : base(message,code)
    {
        
    }
}