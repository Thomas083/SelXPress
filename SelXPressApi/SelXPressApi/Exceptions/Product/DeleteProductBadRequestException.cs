using System.Runtime.Serialization;

namespace SelXPressApi.Exceptions.Product
{
    [Serializable]
    internal class DeleteProductBadRequestException : Exception
    {
        public DeleteProductBadRequestException()
        {
        }

        public DeleteProductBadRequestException(string? message) : base(message)
        {
        }

        public DeleteProductBadRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DeleteProductBadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}