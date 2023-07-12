namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class for the CreateUserBadRequestException's exception
    /// </summary>
    public class CreateUserBadRequestException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public CreateUserBadRequestException(string message) : base(message)
        {
        }
    }
}
