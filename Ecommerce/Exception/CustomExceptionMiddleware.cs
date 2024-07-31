using Microsoft.AspNetCore.Http;
using Microsoft.Owin.Diagnostics.Views;
using System.Net;
using System.Threading.Tasks;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionMiddleware(RequestDelegate next)
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
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Custom error response
            await context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "Bir hata oluştu. Lütfen tekrar deneyin."
            }.ToString());
        }
    }
}
