using System.Net;
using System.Text.Json;
using Firebase.Auth;
using SelXPressApi.Exceptions;

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
            catch (CommonException ex)
            {
                await HandleCommonExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                HttpStatusCode status = HttpStatusCode.InternalServerError;
                
                var exceptionResult = JsonSerializer.Serialize(new { error = ex.Message, status = status, code = "SRV-1000" });
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) status;

                context.Response.WriteAsync(exceptionResult);
            }
            
        }

        private static Task HandleCommonExceptionAsync(HttpContext context, CommonException ex)
        {
            HttpStatusCode status;
            string message = "";
            string code = "";

            var exceptionType = ex.GetType();
            //todo check the type of the exception
            if (exceptionType == typeof(NotFoundException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                code = ex.Code;
            }
            else if (exceptionType == typeof(BadRequestException))
            {
                message = ex.Message;
                status = HttpStatusCode.BadRequest;
                code = ex.Code;
            }
            else if (exceptionType == typeof(UnauthorizedException))
            {
                message = ex.Message;
                status = HttpStatusCode.Unauthorized;
                code = ex.Code;
            }
            else if (exceptionType == typeof(FirebaseAuthException))
            {
                message = ex.Message;
                status = HttpStatusCode.NotFound;
                code = "FRB-1000"; // erreur de firebase au niveau de l'authentification
            }
            else
            {
                message = ex.Message;
                status = HttpStatusCode.InternalServerError;
                code = ex.Code;
            }

            var exceptionResult = JsonSerializer.Serialize(new { error = message, status = status, code = code });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
