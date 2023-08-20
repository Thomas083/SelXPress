namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class of the GetUserByIdBadRequestException's exception
    /// </summary>
    public class GetProductByIdBadRequestException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public GetProductByIdBadRequestException(string message) : base(message)
        {
        }

    }
}
