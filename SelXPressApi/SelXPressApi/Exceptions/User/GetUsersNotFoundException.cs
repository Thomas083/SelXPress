namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class of the GetUsersNotFoundException's exception
    /// </summary>
    public class GetUsersNotFoundException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public GetUsersNotFoundException(string message) : base(message)
        {
        }
    }
}
