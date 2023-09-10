using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Middleware;

/// <summary>
/// Middleware for handling authorization and role-based access control in the SelXPressApi application. 
/// <see cref="IAuthorizationMiddleware"/>
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
public class AuthorizationMiddleware : IAuthorizationMiddleware
{
    private readonly DataContext _context;
    private readonly IUserRepository _userRepository;

	/// <summary>
	/// Initializes a new instance of the <see cref="AuthorizationMiddleware"/> class.
	/// </summary>
	/// <param name="context">The database context used for authorization checks.</param>
	/// <param name="userRepository">The user repository for retrieving user-related data.</param>
	public AuthorizationMiddleware(DataContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
    }
	/// <summary>
	/// Checks if an authorization token exists in the HTTP request and validates it.
	/// </summary>
	/// <param name="context">The HTTP context for the current request.</param>
	/// <returns>True if the token is valid and associated with a user; otherwise, false.</returns>
	/// <exception cref="UnauthorizedException">Thrown if the token is missing, invalid, or not associated with a user.</exception>
	/// <exception cref="NotFoundException">Thrown if the email address in the token is not found in the database.</exception>
	public async Task<bool> CheckIfTokenExists(HttpContext context)
    {
        // check if the token exists and it is not null
        string? authorizationHeader = context.Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            throw new UnauthorizedException("The token is missing, please login you", "SRV-1401");
        }
        // extract the token from the authorization header
        string bearerPrefix = "Bearer ";
        if (authorizationHeader != null && authorizationHeader.StartsWith(bearerPrefix))
        {
            //check the validate of the token
            string jwtToken = authorizationHeader.Substring(bearerPrefix.Length);
            JwtSecurityToken decodedToken;
            if (TryDecodeJwt(jwtToken, out decodedToken))
            {
                var claims = decodedToken.Claims;
                string? email = claims.FirstOrDefault(c => c.Type == "email")?.Value;
                // check if the user exist in the database
                if (await _context.Users.AnyAsync(u => u.Email == email))
                {
                    string emailHeader = "EmailHeader";
                    context.Response.Headers.Add(emailHeader,email);
                    return true;
                }
                throw new NotFoundException("The email address is not in the database", "SRV-1401");
            }
            throw new UnauthorizedException("An error occured while the decoding of the jwt token", "SRV-1701");
        }
        throw new UnauthorizedException("The token is not valid, please try again with another token", "SRV-1702");
    }

	/// <summary>
	/// Checks if the authenticated user has an 'Admin' role.
	/// </summary>
	/// <param name="context">The HTTP context for the current request.</param>
	/// <returns>True if the user has an 'Admin' role; otherwise, false.</returns>
	public async Task<bool> CheckRoleIfAdmin(HttpContext context)
    {
        string? emailValue = context.Response.Headers["EmailHeader"];
        if (emailValue != null)
        {
            User user = await _userRepository.GetUserByEmail(emailValue);
            if (user.Role.Name == RoleType.Operator.ToString())
                return true;
            return false;
        }
        return false;
    }

	/// <summary>
	/// Checks if the authenticated user has a 'Customer' role.
	/// </summary>
	/// <param name="context">The HTTP context for the current request.</param>
	/// <returns>True if the user has a 'Customer' role; otherwise, false.</returns>
	public async Task<bool> CheckRoleIfCustomer(HttpContext context)
    {
        string? emailValue = context.Response.Headers["EmailHeader"];
        if (emailValue != null)
        {
            User user = await _userRepository.GetUserByEmail(emailValue);
            if (user.Role.Name == RoleType.Customer.ToString())
                return true;
            return false;
        }
        return false;
    }

	/// <summary>
	/// Checks if the authenticated user has a 'Seller' role.
	/// </summary>
	/// <param name="context">The HTTP context for the current request.</param>
	/// <returns>True if the user has a 'Seller' role; otherwise, false.</returns>
	public async Task<bool> CheckRoleIfSeller(HttpContext context)
    {
        string? emailValue = context.Response.Headers["EmailHeader"];
        if (emailValue != null)
        {
            User user = await _userRepository.GetUserByEmail(emailValue);
            if (user.Role.Name == RoleType.Seller.ToString())
                return true;
            return false;
        }
        return false;
    }

    private bool TryDecodeJwt(string jwtToken,out JwtSecurityToken decodedToken)
    {
        decodedToken = null;
        var tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            if (tokenHandler.CanReadToken(jwtToken))
            {
                var securityToken = tokenHandler.ReadJwtToken(jwtToken);
                decodedToken = securityToken;
                return true;
            }

            return false;
        }
        catch (Exception ex)
        {
            throw new UnauthorizedException("An error occured while the decoding of the jwt token", "SRV-1701");
        }
    }
}