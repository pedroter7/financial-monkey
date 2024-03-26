using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace PedroTer7.FinancialMonkey.Common;

public static class WebApplicationExtensions
{
    public static WebApplication UseFinancialMonkeyExceptionHandlingMiddleware(this WebApplication app)
    {
        app.Use(async (ctx, next) =>
        {
            try
            {
                await next(ctx);
            }
            catch (UnauthorizedAccessException)
            {
                ctx.Response.Clear();
                ctx.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
            catch (Exception e)
            {
                Log.Error(e, "Unhandled exception");
                ctx.Response.Clear();
                ctx.Response.StatusCode = StatusCodes.Status500InternalServerError;
                return;
            }
        });

        return app;
    }
}
