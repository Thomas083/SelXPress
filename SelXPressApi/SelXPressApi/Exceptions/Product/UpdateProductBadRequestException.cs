using System.Runtime.Serialization;

namespace SelXPressApi.Exceptions.Product
{
    [Serializable]
    internal class UpdateProductBadRequestException : Exception
    {
        public UpdateProductBadRequestException()
        {
        }

        public UpdateProductBadRequestException(string? message) : base(message)
        {
        }

        public UpdateProductBadRequestException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UpdateProductBadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}