namespace SelXPressApi.Exceptions
{
	/// <summary>
	/// Exception class for unauthorized access.
	/// </summary>
	public class UnauthorizedException : CommonException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnauthorizedException"/> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="code">The error code.</param>
		public UnauthorizedException(string message, string code) : base(message, code)
		{
		}
	}
}
