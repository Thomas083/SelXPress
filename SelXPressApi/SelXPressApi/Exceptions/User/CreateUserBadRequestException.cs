namespace SelXPressApi.Exceptions.User
{
    public class CreateUserBadRequestException : CommonException
    {
        public CreateUserBadRequestException(string message, string code, int status) : base(message, code, status)
        {
        }
    }
}
