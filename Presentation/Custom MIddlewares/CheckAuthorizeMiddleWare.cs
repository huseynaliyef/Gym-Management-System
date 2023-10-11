using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presentation.Custom_MIddlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class CheckAuthorizeMiddleWare
    {
        private readonly RequestDelegate _next;

        public CheckAuthorizeMiddleWare(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {

            await _next(httpContext);

            if (httpContext.Response.StatusCode == 401)
            {
                var result = JsonSerializer.Serialize(new { error = "Unauthorized access." });
                httpContext.Response.ContentType = "application/json";

                await httpContext.Response.WriteAsync(result);
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ResponseMiddleWareExtensions
    {
        public static IApplicationBuilder UseResponseMiddleWare(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CheckAuthorizeMiddleWare>();
        }
    }
}
