namespace SelXPressApi.Exceptions
{
	/// <summary>
	/// Exception class for representing bad request errors.
	/// </summary>
	public class BadRequestException : CommonException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="BadRequestException"/> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="code">The error code.</param>
		public BadRequestException(string message, string code) : base(message, code)
		{

		}
	}
}
