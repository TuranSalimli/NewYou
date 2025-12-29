using System.Net;
using NewYou.Domain.Exceptions;
namespace NewYou.API.Middlewares;
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        context.Response.StatusCode = exception switch
        {
            ArgumentException => (int)HttpStatusCode.BadRequest,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            InvalidOperationException => (int)HttpStatusCode.Forbidden,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            ApplicationException => (int)HttpStatusCode.ExpectationFailed,
            _ => (int)HttpStatusCode.BadRequest
        };

        if (exception.GetType().IsGenericType)
        {
            var genericType = exception.GetType().GetGenericTypeDefinition();

            //generics
        }
        else
        {
            switch (exception)
            {
                case LoginFailedException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case RegisterFailedException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
            }
        }

        var response = new { message = exception.Message };

        return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(response));
    }
}
