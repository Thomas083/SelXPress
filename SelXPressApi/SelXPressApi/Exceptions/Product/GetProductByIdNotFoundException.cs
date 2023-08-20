namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class for the GetUserByIdNotFoundException's exception
    /// </summary>
    public class GetProductByIdNotFoundException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public GetProductByIdNotFoundException(string message) : base(message)
        {
        }
    }
}
