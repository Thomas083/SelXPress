using System.Net;
using System.Text.Json;
using Firebase.Auth;
using SelXPressApi.Exceptions;

namespace SelXPressApi.Configurations
{
	/// <summary>
	/// Middleware for global exception handling in the application. 
	/// This middleware captures and handles various types of exceptions,
	/// returning appropriate JSON error responses.
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
	public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

		/// <summary>
		/// Initializes a new instance of the <see cref="GlobalExceptionHandlingMiddleware"/> class.
		/// </summary>
		/// <param name="next">The next middleware in the pipeline.</param>
		public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

		/// <summary>
		/// Invokes the middleware to handle exceptions and pass the request to the next middleware in the pipeline.
		/// </summary>
		/// <param name="context">The HTTP context for the current request.</param>
		/// <returns>A task representing the asynchronous operation.</returns>
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
            else if (exceptionType == typeof(ForbiddenRequestException))
            {
                message = ex.Message;
                status = HttpStatusCode.Forbidden;
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
