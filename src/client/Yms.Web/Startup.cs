using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http.Headers;
using Yms.Common.Contracts;
using Yms.Web.HttpHandlers;
using Yms.Web.Middlewares;

namespace Yms.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpClient<IYmsApiHttpHandler, YmsApiHttpHandler>(c =>
            {
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                c.DefaultRequestHeaders.Add("X-Client-Type", "YMS-MVC-Client");
                c.BaseAddress = new Uri(Configuration.GetValue<string>("ApiUrl"));
            });
            services.AddScoped<IClaims, UserClaims>();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMiddleware<GCMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            app.UseMiddleware<CheckUserClaimsMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}