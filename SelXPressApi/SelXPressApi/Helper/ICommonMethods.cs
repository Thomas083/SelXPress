using System.Threading.Tasks;

namespace SelXPressApi.Helper
{
	/// <summary>
	/// Interface for invoking common methods.
	/// </summary>
	public interface ICommonMethods
	{
		/// <summary>
		/// Saves modifications in the database.
		/// </summary>
		/// <returns>A task representing the asynchronous save operation.</returns>
		Task<bool> Save();
	}
}
