namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class for the GetUserByIdNotFoundException's exception
    /// </summary>
    public class GetUserByIdNotFoundException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public GetUserByIdNotFoundException(string message) : base(message)
        {
        }
    }
}
