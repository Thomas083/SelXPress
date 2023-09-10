using System.Threading.Tasks;

namespace SelXPressApi.Helper
{
	/// <summary>
	/// Interface for invoking common methods.
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
	public interface ICommonMethods
	{
		/// <summary>
		/// Saves modifications in the database.
		/// </summary>
		/// <returns>A task representing the asynchronous save operation.</returns>
		Task<bool> Save();
	}
}
