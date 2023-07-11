namespace SelXPressApi.Exceptions.Product
{
    public class GetProductNotFoundException : CommonException
    {
        public GetProductNotFoundException(string message, string code, int status) : base(message, code, status)
        {
            
        }
    }
}
