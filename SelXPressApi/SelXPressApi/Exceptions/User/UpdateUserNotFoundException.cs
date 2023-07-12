namespace SelXPressApi.Exceptions.User
{
    /// <summary>
    /// Class for the UpdateUserNotFoundException's exception
    /// </summary>
    public class UpdateUserNotFoundException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"></param>
        public UpdateUserNotFoundException(string message) : base(message)
        {
        }
    }
}
