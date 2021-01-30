using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yms.Api.Middlewares
{
    public class CheckHeaderMiddleware
    {
        private readonly RequestDelegate next;
        public CheckHeaderMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Headers.ContainsKey("X-Client-Type"))
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsync("Unknown client");
                return;
            }

            await next(httpContext);
        }
    }
}
