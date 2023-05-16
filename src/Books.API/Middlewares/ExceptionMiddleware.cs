using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Books.API.Middlewares
{
    public static class ExceptionMiddleware
    {
        public static IApplicationBuilder UseExceptionMiddlware(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    // TODO: log exception
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsJsonAsync(new ProblemDetails()
                        {
                            Status = context.Response.StatusCode,
                            Type = "InternalServerError",
                            Detail = "Something went wrong and the request could not be handled."
                        });
                    }
                });
            });
            return app;
        }
    }
}
