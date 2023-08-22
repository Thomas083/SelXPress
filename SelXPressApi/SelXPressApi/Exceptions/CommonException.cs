namespace SelXPressApi.Exceptions
{
	/// <summary>
	/// Base exception class with additional properties for common exception handling.
	/// </summary>
	public class CommonException : Exception
	{
		/// <summary>
		/// Gets the error code associated with the exception.
		/// </summary>
		public string Code { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="CommonException"/> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="code">The error code.</param>
		public CommonException(string message, string code) : base(message)
		{
			Code = code;
		}
	}
}
