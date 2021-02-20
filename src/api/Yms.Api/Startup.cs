using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Yms.Api.Helpers;
using Yms.Api.Middlewares;
using Yms.Common.Caching.Extensions;
using Yms.Common.Contracts;
using Yms.Data.Context.Extensions;
using Yms.Services.Common.Extensions;
using Yms.Services.OrderManagement.Extensions;
using Yms.Services.Production.Extensions;

namespace Yms.Api
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
            services.Configure<Settings>(Configuration.GetSection("Settings"));
            services.AddCors(options => options.AddPolicy("all", pb => { pb.AllowAnyOrigin().AllowAnyHeader(); })); // ne yapacağımı bilmediğimden all yaptım
            services.AddDataContext(Configuration.GetSection("Settings:Database:Default").Value);
            services.AddProductionServices().AddOrderServices().AddCommonServices();
            services.AddScoped<IClaims, UserClaims>();
            services.AddControllers();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Settings:Authentication:Secret").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddAuthorization();
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<AddClientTypeHeaderParameter>();
            });
            services.AddCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "YMS API");
            });

            //Proje çalışmadan önce otomatik olarak migrate edilecek
            //using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            //{
            //    var context = serviceScope.ServiceProvider.GetRequiredService<DbContext>();
            //    context.Database.Migrate();
            //}
            app.UseCors("all");
            app.UseMiddleware<CheckHeaderMiddleware>();
            //app.UseMiddleware<LogRequestMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<CheckUserClaimsMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(name: "Area", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
        }
    }
}