namespace SelXPressApi.Exceptions
{
	/// <summary>
	/// Exception class for unauthorized access.
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
