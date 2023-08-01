using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SelXPressApi.Data;
using SelXPressApi.Exceptions;

namespace SelXPressApi.Middleware;

public class AuthorizationMiddleware : IAuthorizationMiddleware
{
    private readonly DataContext _context;

    public AuthorizationMiddleware(DataContext context)
    {
        _context = context;
    }
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
                string? email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                // check if the user exist in the database
                if (await _context.Users.AnyAsync(u => u.Email == email))
                {
                    return true;
                }
                return false;
            }
            throw new BadRequestException("An error occured while the decoding of the jwt token", "SRV-1465");
        }
        throw new BadRequestException("The token is not valid, please try again with another token", "SRV-1402");
    }

    private bool TryDecodeJwt(string jwtToken,out JwtSecurityToken decodedToken)
    {
        decodedToken = null;
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = false
        };
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
            throw new BadRequestException("An error occured while the decoding of the jwt token", "SRV-");
        }
    }
}