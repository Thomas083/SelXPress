namespace SelXPressApi.ErrorMessage
{
    public class ServerErrorMessage
    {
        private string Message { get; set; }
        private int Code { get; set; }
        private int Status { get; set; }

        public ServerErrorMessage(string message, int code)
        {
            Message = message;
            Code = code;
            Status = 500;
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
