namespace SelXPressApi.ErrorMessage
{
    public class BadRequestErrorMessage
    {
        private string Message { get; set; }
        private int Code { get; set; }
        private int Status { get; set; }

        public BadRequestErrorMessage(string message, int code)
        {
            Message = message;
            Code = code;
            Status = 400;
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
