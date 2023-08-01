namespace SelXPressApi.Exceptions;

public class NotFoundException : CommonException
{
    public NotFoundException(string message, string code) : base(message,code)
    {
    }
}