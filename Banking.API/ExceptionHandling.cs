using Banking.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Banking.API;

public static class ExceptionHandling
{
    public static void UseGlobalExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
            exceptionHandlerApp.Run(async context =>
            {
                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                var (statusCode, message) = exception switch
                {
                    AccountNotFoundException => (StatusCodes.Status404NotFound, exception.Message),
                    _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred.")
                };

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(new { Error = message });
            }));
    }
}
