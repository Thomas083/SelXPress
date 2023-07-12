namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class for the UpdateUserBadRequestException's exception
    /// </summary>
    public class UpdateUserBadRequestException : CommonException
    {
        /// <summary>
        /// Constructor for the UpdateUserBadRequestException's exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="status"></param>
        public UpdateUserBadRequestException(string message, string code, int status) : base(message, code, status)
        {
        }
    }
}
