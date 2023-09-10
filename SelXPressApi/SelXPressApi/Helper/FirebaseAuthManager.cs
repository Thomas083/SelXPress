using Firebase.Auth;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SelXPressApi.Helper
{
	/// <summary>
	/// Manager for Firebase authentication.
	/// <see cref="IFirebaseAuthManager"/>
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
	public class FirebaseAuthManager : IFirebaseAuthManager
	{
		private readonly FirebaseAuthProvider _authProvider;

		/// <summary>
		/// Constructor for initializing the FirebaseAuthManager class.
		/// </summary>
		public FirebaseAuthManager()
		{
			_authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBl6nMs-CAUM4Drx39lGvBdlJrQsyfEEmA"));
		}

		/// <summary>
		/// Logs in a user using their email and password.
		/// </summary>
		/// <param name="email">The user's email.</param>
		/// <param name="password">The user's password.</param>
		/// <returns>The Firebase token for the authenticated user.</returns>
		public async Task<string> LoginWithEmailAndPasswordAsync(string email, string password)
		{
			var auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
			return auth.FirebaseToken;
		}

		/// <summary>
		/// Creates a new user account using their email and password.
		/// </summary>
		/// <param name="email">The user's email.</param>
		/// <param name="password">The user's password.</param>
		/// <returns>The Firebase token for the newly created user.</returns>
		public async Task<string> CreateWithEmailAndPasswordAsync(string email, string password)
		{
			var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
			return auth.FirebaseToken;
		}

		/// <summary>
		/// Logs in a user and retrieves a refresh token.
		/// </summary>
		/// <param name="email">The user's email.</param>
		/// <param name="password">The user's password.</param>
		/// <returns>The refresh token for the authenticated user.</returns>
		public async Task<string> LoginWithEmailAndPasswordRefreshAsync(string email, string password)
		{
			var auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
			return auth.RefreshToken;
		}
	}
}
