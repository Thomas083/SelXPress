namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class of the DeleteUserNotFoundException's exception
    /// </summary>
    public class DeleteUserNotFoundException : CommonException
    {
        /// <summary>
        /// Constructor of the DeleteUserNotFoundException's exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="status"></param>
        public DeleteUserNotFoundException(string message, string code, int status) : base(message, code, status)
        {
        }
    }
}
