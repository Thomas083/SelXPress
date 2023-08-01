namespace SelXPressApi.Middleware;

public interface IAuthorizationMiddleware
{
    Task<bool> CheckIfTokenExists(HttpContext context);
    Task<bool> CheckRoleIfAdmin(HttpContext context);
    Task<bool> CheckRoleIfCustomer(HttpContext context);
    Task<bool> CheckRoleIfSeller(HttpContext context);
}