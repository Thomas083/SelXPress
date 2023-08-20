namespace SelXPressApi.Exceptions.Product
/// <summary>
/// Class of the GetProductsNotFoundException's exception
/// </summary>
{
    public class GetProductsNotFoundException : Exception
    {
        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="message"> Message of the exception </param>
        public GetProductsNotFoundException(string message) : base(message)
        {
            
        }
    }
}
