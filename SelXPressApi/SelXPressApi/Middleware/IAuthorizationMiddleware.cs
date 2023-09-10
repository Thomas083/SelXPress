namespace SelXPressApi.Middleware;

/// <summary>
/// Defines the contract for handling authorization and role-based access control in the SelXPressApi application.
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
public interface IAuthorizationMiddleware
{
	/// <summary>
	/// Checks if an authorization token exists in the HTTP request and validates it.
	/// </summary>
	/// <param name="context">The HTTP context for the current request.</param>
	/// <returns>True if the token is valid and associated with a user; otherwise, false.</returns>
	Task<bool> CheckIfTokenExists(HttpContext context);

	/// <summary>
	/// Checks if the authenticated user has an 'Admin' role.
	/// </summary>
	/// <param name="context">The HTTP context for the current request.</param>
	/// <returns>True if the user has an 'Admin' role; otherwise, false.</returns>
	Task<bool> CheckRoleIfAdmin(HttpContext context);

	/// <summary>
	/// Checks if the authenticated user has a 'Customer' role.
	/// </summary>
	/// <param name="context">The HTTP context for the current request.</param>
	/// <returns>True if the user has a 'Customer' role; otherwise, false.</returns>
	Task<bool> CheckRoleIfCustomer(HttpContext context);

	/// <summary>
	/// Checks if the authenticated user has a 'Seller' role.
	/// </summary>
	/// <param name="context">The HTTP context for the current request.</param>
	/// <returns>True if the user has a 'Seller' role; otherwise, false.</returns>
	Task<bool> CheckRoleIfSeller(HttpContext context);
}
