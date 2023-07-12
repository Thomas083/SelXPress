namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class of the GetUsersNotFoundException's exception
    /// </summary>
    public class GetUsersNotFoundException : Exception
    {
        private string Code { get; set; }
        public GetUsersNotFoundException(string message, string code) : base(message)
        {
            Code = code;
        }

        public string GetCode()
        {
            return Code;
        }
    }
}
