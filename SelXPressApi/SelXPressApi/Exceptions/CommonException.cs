namespace SelXPressApi.Exceptions
{
    /// <summary>
    /// Class of the common exception the most of the exception inherit of it
    /// </summary>
    public class CommonException : Exception
    {
        /// <summary>
        /// Message of the exception
        /// </summary>
        private string Message { get; set; }

        /// <summary>
        /// Code of the error to allow us to identify the error
        /// </summary>
        private string Code { get; set; }

        /// <summary>
        /// Status Code HTTP of the request
        /// </summary>
        private int Status { get; set; }

        /// <summary>
        /// Constructor of the exception
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <param name="status"></param>
        public CommonException(string message, string code, int status)
        {
            Message = message;
            Code = code;
            Status = status;
        }

        /// <summary>
        /// Method to return the message of the exception
        /// </summary>
        /// <returns></returns>
        public string GetMessage()
        {
            return Message;
        }

        /// <summary>
        /// Method to return the code of the exception
        /// </summary>
        /// <returns></returns>
        public string GetCode()
        {
            return Code;
        }

        /// <summary>
        /// Method to return the status of the request HTTP
        /// </summary>
        /// <returns></returns>
        public int GetStatus()
        {
            return Status;
        }
    }
}
