using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Yms.Common.Contracts;

namespace Yms.Web.Middlewares
{
    public class CheckUserClaimsMiddleware
    {
        private readonly RequestDelegate next;
        public CheckUserClaimsMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, IClaims claims)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                claims.IsAuthenticated = true;
                claims.Session = new UserSessionData
                {
                    Id = Guid.Parse(httpContext.User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value),
                    UserName = httpContext.User.Claims.First(i => i.Type == ClaimTypes.Name).Value,
                    DisplayName = httpContext.User.Claims.First(i => i.Type == ClaimTypes.GivenName).Value
                };
                claims.Session.Extras = new Dictionary<string, object>
                {
                    { Constants.JwtTokenName, httpContext.User.Claims.First(i => i.Type == Constants.JwtTokenName).Value }
                };
            }
            await next(httpContext);
        }
    }
}
