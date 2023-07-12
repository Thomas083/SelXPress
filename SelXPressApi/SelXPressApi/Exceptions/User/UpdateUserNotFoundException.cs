namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class for the UpdateUserNotFoundException's exception
    /// </summary>
    public class UpdateUserNotFoundException : CommonException
    {
        /// <summary>
        /// Constructor for the UpdateUserNotFoundException's exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="status"></param>
        public UpdateUserNotFoundException(string message, string code, int status) : base(message, code, status)
        {
        }
    }
}
