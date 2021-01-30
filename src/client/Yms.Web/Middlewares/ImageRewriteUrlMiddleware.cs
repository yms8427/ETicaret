using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Yms.Web.Middlewares
{
    public class ImageRewriteUrlMiddleware
    {
        private readonly RequestDelegate _next;

        public ImageRewriteUrlMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("/image/"))
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://ymsseksendortyirmiyedi.blob.core.windows.net");
                client.DefaultRequestHeaders.Add("azure-subscription-key", "fhsdfkljsdhfwe8wekljfhweofhwelfweofhweflwefol");
                client.DefaultRequestHeaders.Add("azure-subscription-pass", "?'^+'^+()2342342c3239tyv45klj^+cc234r");
                var response = await client.GetAsync("ymsblob/product-5.jpg");
                context.Response.StatusCode = StatusCodes.Status200OK;

                var stream = await response.Content.ReadAsStreamAsync();
                await context.Response.WriteAsync(await response.Content.ReadAsStringAsync());
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                context.Response.Body.CopyTo(stream);
                context.Response.Body = stream;
            }

            await _next(context);
        }
    }
}
