
using Microsoft.AspNetCore.Builder;
using MyAspCoreRestAPI.MyCustMiddleware;

namespace MyAspCoreRestAPI.MyMiddlewareExtensions
{
    public static class MySample1MiddlewareExtensions
    {
        public static IApplicationBuilder UseMySample1Middleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MySample1Middleware>();
        }
    }
}
