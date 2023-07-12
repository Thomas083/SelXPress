namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class for the GetUserByIdNotFoundException's exception
    /// </summary>
    public class GetUserByIdNotFoundException : CommonException
    {
        /// <summary>
        /// Constructor for the GetUserByNotFoundException's exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="status"></param>
        public GetUserByIdNotFoundException(string message, string code, int status) : base(message, code, status)
        {
        }
    }
}
