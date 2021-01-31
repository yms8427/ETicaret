using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Yms.Web.Middlewares
{
    public class GCMiddleware
    {
        private readonly RequestDelegate _next;

        public GCMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);
            GC.Collect(2, GCCollectionMode.Forced, true);
            GC.WaitForPendingFinalizers();
        }
    }
}
