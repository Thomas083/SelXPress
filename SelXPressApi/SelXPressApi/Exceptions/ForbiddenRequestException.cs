namespace SelXPressApi.Exceptions
{
	/// <summary>
	/// Exception class for forbidden requests.
	/// </summary>
	public class ForbiddenRequestException : CommonException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ForbiddenRequestException"/> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="code">The error code.</param>
		public ForbiddenRequestException(string message, string code) : base(message, code)
		{
		}
	}
}
