using BaseApi.Handlers.Kernel;
using System.Net;

namespace BaseApi.Middlewares;

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
        catch (HandlerException exception)
        {
            await HandleHandlerExceptionAsync(context, exception);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private static Task HandleHandlerExceptionAsync(HttpContext context, HandlerException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exception.StatusCode!;

        return context.Response.WriteAsJsonAsync(new { message = exception.Message });
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception switch
        {
            ArgumentNullException => (int)HttpStatusCode.BadRequest,
            KeyNotFoundException => (int)HttpStatusCode.NotFound,
            _ => (int)HttpStatusCode.InternalServerError,
        };

        return context.Response.WriteAsJsonAsync(new { message = $"{exception.GetType()}: {exception.Message}" });
    }
}
