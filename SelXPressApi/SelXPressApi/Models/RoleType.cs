namespace SelXPressApi.Models;

/// <summary>
/// Represents the available roles in the SelXPressApi application.
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
public enum RoleType
{
	/// <summary>
	/// Represents the 'Operator' role.
	/// </summary>
	Operator,

	/// <summary>
	/// Represents the 'Seller' role.
	/// </summary>
	Seller,

	/// <summary>
	/// Represents the 'Customer' role.
	/// </summary>
	Customer
}