namespace SelXPressApi.Exceptions.Product
{
    public class GetProductBadRequestException : CommonException
    {
        public GetProductBadRequestException(string message, string code, int status) : base(message, code, status)
        {
            
        }
    }
}
