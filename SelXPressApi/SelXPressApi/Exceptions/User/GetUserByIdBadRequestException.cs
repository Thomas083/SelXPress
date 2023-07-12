namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class of the GetUserByIdBadRequestException's exception
    /// </summary>
    public class GetUserByIdBadRequestException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public GetUserByIdBadRequestException(string message) : base(message)
        {
        }

    }
}
