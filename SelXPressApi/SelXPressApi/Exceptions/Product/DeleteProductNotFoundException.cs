using System.Runtime.Serialization;

namespace SelXPressApi.Exceptions.Product
{
    [Serializable]
    internal class DeleteProductNotFoundException : Exception
    {
        public DeleteProductNotFoundException()
        {
        }

        public DeleteProductNotFoundException(string? message) : base(message)
        {
        }

        public DeleteProductNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected DeleteProductNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}