namespace SelXPressApi.Configurations
{
	/// <summary>
	/// Adds a global error handling middleware to the application pipeline.
	/// This middleware captures unhandled exceptions and provides a centralized
	/// error handling mechanism for the application.
	/// <see cref="GlobalExceptionHandlingMiddleware"/>
	/// </summary>
	/// <param name="applicationBuilder">The <see cref="IApplicationBuilder"/> instance.</param>
	/// <returns>The <see cref="IApplicationBuilder"/> instance with the middleware added.</returns>
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
	public static class ApplicationBuilderExtensions
    {
		/// <summary>
		/// Extension method to add global error handling middleware to the application pipeline.
		/// </summary>
		/// <param name="applicationBuilder">The application builder.</param>
		/// <returns>The application builder with the global error handling middleware added.</returns>
		public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}
