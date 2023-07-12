namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class of the GetUsersBadRequestExeption's exception
    /// </summary>
    public class GetUsersBadRequestException : Exception
    {
        private string Code { get; set; }
        
        public GetUsersBadRequestException(string message,string code) : base(message)
        {
            Code = code;
        }

        public string GetCode()
        {
            return Code;
        }
    }
}
