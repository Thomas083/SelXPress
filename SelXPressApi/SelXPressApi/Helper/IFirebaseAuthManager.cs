using System.Threading.Tasks;

namespace SelXPressApi.Helper
{
	/// <summary>
	/// Interface for managing Firebase authentication.
	/// </summary>
	public interface IFirebaseAuthManager
	{
		/// <summary>
		/// Logs in a user with the given email and password and returns the authentication token.
		/// </summary>
		/// <param name="email">User's email.</param>
		/// <param name="password">User's password.</param>
		/// <returns>A task representing the asynchronous login operation and the authentication token.</returns>
		Task<string> LoginWithEmailAndPasswordAsync(string email, string password);
	}
}
