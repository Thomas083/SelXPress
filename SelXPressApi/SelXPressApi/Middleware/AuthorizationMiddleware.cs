﻿using System.IdentityModel.Tokens.Jwt;
using Microsoft.EntityFrameworkCore;
using SelXPressApi.Data;
using SelXPressApi.Exceptions;
using SelXPressApi.Interfaces;
using SelXPressApi.Models;

namespace SelXPressApi.Middleware;

public class AuthorizationMiddleware : IAuthorizationMiddleware
{
    private readonly DataContext _context;
    private readonly IUserRepository _userRepository;

    public AuthorizationMiddleware(DataContext context, IUserRepository userRepository)
    {
        _context = context;
        _userRepository = userRepository;
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

    public async Task<bool> CheckRoleIfAdmin(HttpContext context)
    {
        string? emailValue = context.Request.Headers["EmailHeader"];
        if (emailValue != null)
        {
            User user = await _userRepository.GetUserByEmail(emailValue);
            if (user.Role.Name == RoleType.Operator.ToString())
                return true;
            return false;
        }
        return false;
    }

    public async Task<bool> CheckRoleIfCustomer(HttpContext context)
    {
        string? emailValue = context.Request.Headers["EmailHeader"];
        if (emailValue != null)
        {
            User user = await _userRepository.GetUserByEmail(emailValue);
            if (user.Role.Name == RoleType.Customer.ToString())
                return true;
            return false;
        }
        return false;
    }

    public async Task<bool> CheckRoleIfSeller(HttpContext context)
    {
        string? emailValue = context.Request.Headers["EmailHeader"];
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