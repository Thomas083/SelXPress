namespace SelXPressApi.Exceptions.Product
{
    public class GetProductsBadRequestException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public GetProductsBadRequestException(string message) : base(message)
        {
            
        }
    }
}
