using Microsoft.AspNetCore.Mvc.Controllers;
using SelXPressApi.Exceptions.User;
using System.Net;
using System.Text.Json;

namespace SelXPressApi.Configurations
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            HttpStatusCode status;
            string message = "";
            string code = "";

            var exceptionType = ex.GetType();
            if(exceptionType == typeof(CreateUserBadRequestException))
            {
                message = ex.Message;
                status = HttpStatusCode.BadRequest;
                code = "USR-1000";
            }
            else if(exceptionType == typeof(DeleteUserBadRequestException))
            {
                message = ex.Message;
                status = HttpStatusCode.BadRequest;
                code = "USR-1001";
            }
            else if(exceptionType == typeof(DeleteUserNotFoundException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                code = "USR-1002";
            }
            else if(exceptionType == typeof(GetUserByIdBadRequestException))
            {
                message = ex.Message;
                status = HttpStatusCode.BadRequest;
                code = "USR-1003";
            }
            else if (exceptionType == typeof(GetUsersNotFoundException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                code = "USR-1004";
            }
            else if (exceptionType == typeof(GetUsersBadRequestException))
            {
                message = ex.Message;
                status = HttpStatusCode.BadRequest;
                code = "USR-1005";
            }
            else if (exceptionType == typeof(GetUsersNotFoundException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                code = "USR-1006";
            }
            else if (exceptionType == typeof(UpdateUserBadRequestException))
            {
                message = ex.Message;
                status = HttpStatusCode.BadRequest;
                code = "USR-1007";
            }
            else if (exceptionType == typeof(UpdateUserNotFoundException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                code = "USR-1008";
            }
            else
            {
                message = ex.Message;
                status = HttpStatusCode.InternalServerError;
                code = "SRV-1000";
            }

            var exceptionResult = JsonSerializer.Serialize(new { error = message, status = status, code = code });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
