namespace SelXPressApi.Exceptions;

public class CommonException : Exception
{
    public string Code { get; set; }
    public CommonException(string message, string code) : base(message)
    {
        Code = code;
    }
}