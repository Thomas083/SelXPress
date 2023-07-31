using Firebase.Auth;
using Microsoft.AspNetCore.Identity;

namespace SelXPressApi.Helper;

public class FirebaseAuthManager : IFirebaseAuthManager
{
    private readonly FirebaseAuthProvider _authProvider;

    public FirebaseAuthManager()
    {
        _authProvider = new FirebaseAuthProvider(new FirebaseConfig("AIzaSyBl6nMs-CAUM4Drx39lGvBdlJrQsyfEEmA"));
    }

    public async Task<string> LoginWithEmailAndPasswordAsync(string email, string password)
    {
        var auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
        return auth.FirebaseToken;
    }

    public async Task<string> CreateWithEmailAndPasswordAsync(string email, string password)
    {
        var auth = await _authProvider.CreateUserWithEmailAndPasswordAsync(email, password);
        return auth.FirebaseToken;
    }

    public async Task<string> LoginWithEmailAndPasswordRefreshAsync(string email, string password)
    {
        var auth = await _authProvider.SignInWithEmailAndPasswordAsync(email, password);
        return auth.RefreshToken;
    }
}