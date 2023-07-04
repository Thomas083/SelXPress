namespace SelXPressApi.ErrorMessage
{
    public class UnauthorizedErrorMessage
    {
        private string Message { get; set; }
        private int Code { get; set; }
        private int Status { get; set; }

        public UnauthorizedErrorMessage(string message, int code)
        {
            Message = message;
            Code = code;
            Status = 401;
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
