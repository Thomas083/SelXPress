namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class for the CreateUserBadRequestException's exception
    /// </summary>
    public class CreateProductBadRequestException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public CreateProductBadRequestException(string message) : base(message)
        {
        }
    }
}
