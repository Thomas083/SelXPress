namespace SelXPressApi.Exceptions
{
	/// <summary>
	/// Base exception class with additional properties for common exception handling.
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
