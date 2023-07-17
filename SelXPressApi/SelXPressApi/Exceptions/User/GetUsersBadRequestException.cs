namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class of the GetUsersBadRequestExeption's exception
    /// </summary>
    public class GetUsersBadRequestException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public GetUsersBadRequestException(string message) : base(message)
        {
        }
    }
}
