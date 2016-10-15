using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;


namespace MyAspCoreRestAPI.MyCustMiddleware
{
    public class MySample1Middleware
    {
        private readonly RequestDelegate _next;
        
        public MySample1Middleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if( context.Request.Headers.ContainsKey("X-MyAuthTest"))
            {
                if (context.Request.Headers["X-MyAuthTest"].Equals("false"))
                {
                    context.Response.StatusCode = 401;
                    return;
                }
            }
            await _next.Invoke(context);
        }
    }

}
