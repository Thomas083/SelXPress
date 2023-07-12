namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class of the DeleteUserNotFoundException's exception
    /// </summary>
    public class DeleteUserNotFoundException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public DeleteUserNotFoundException(string message) : base(message)
        {
        }
    }
}
