namespace SelXPressApi.Helper;

public interface IFirebaseAuthManager
{
    Task<string> LoginWithEmailAndPasswordAsync(string email, string password);
}