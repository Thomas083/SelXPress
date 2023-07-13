namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class for the UpdateUserBadRequestException's exception
    /// </summary>
    public class UpdateUserBadRequestException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public UpdateUserBadRequestException(string message) : base(message)
        {
        }
    }
}
