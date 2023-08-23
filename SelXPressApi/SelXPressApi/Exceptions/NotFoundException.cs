namespace SelXPressApi.Exceptions
{
	/// <summary>
	/// Exception class for not found resources.
	/// </summary>
	public class NotFoundException : CommonException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NotFoundException"/> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		/// <param name="code">The error code.</param>
		public NotFoundException(string message, string code) : base(message, code)
		{
		}
	}
}
