namespace SelXPressApi.ErrorMessage
{
    public class NotFoundErrorMessage
    {
        private string Message { get; set; }
        private int Code { get; set; }

        private int Status { get; set; }

        public NotFoundErrorMessage(string message, int code)
        {
            Message = message;
            Code = code;
            Status = 404;
        }

        public string GetMessage()
        {
            return Message;
        }

        public int GetCode()
        {
            return Code;
        }

        public int GetStatus()
        {
            return Status;
        }
    }
}
