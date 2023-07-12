namespace SelXPressApi.Exceptions.User
{
    public class GetUserByIdBadRequestException : CommonException
    {
        public GetUserByIdBadRequestException(string message, string code, int status) : base(message, code, status)
        {
        }
    }
}
