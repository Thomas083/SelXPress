namespace SelXPressApi.Exceptions
{
	/// <summary>
	/// Exception class for representing bad request errors.
	/// </summary>
	/// <seealso  cref="Models"/>
	/// <seealso  cref="DTO"/>
	/// <seealso  cref="Controllers"/>
	/// <seealso  cref="Repository"/>
	/// <seealso  cref="Helper"/>
	/// <seealso  cref="DocumentationErrorTemplate"/>
	/// <seealso  cref="Exceptions"/>
	/// <seealso  cref="Interfaces"/>
	/// <seealso  cref="Middleware"/>
	/// <seealso  cref="Data"/>
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
