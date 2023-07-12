namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class of the DeleteUserBadRequestException's exception
    /// </summary>
    public class DeleteUserBadRequestException : CommonException
    {
        /// <summary>
        /// Constructor of the DeleteUserBadRequestException's exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="status"></param>
        public DeleteUserBadRequestException(string message, string code, int status) : base(message, code, status)
        {
        }
    }
}
