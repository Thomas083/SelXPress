namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class of the DeleteUserBadRequestException's exception
    /// </summary>
    public class DeleteUserBadRequestException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public DeleteUserBadRequestException(string message) : base(message)
        {
        }
    }
}
