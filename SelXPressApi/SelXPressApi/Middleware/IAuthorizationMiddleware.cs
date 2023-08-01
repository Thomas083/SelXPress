namespace SelXPressApi.Middleware;

public interface IAuthorizationMiddleware
{
    Task<bool> CheckIfTokenExists(HttpContext context);
    
}