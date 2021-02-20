using Microsoft.Extensions.DependencyInjection;
using Yms.Common.Caching.Abstractions;
using Yms.Common.Caching.Concretes;

namespace Yms.Common.Caching.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<ICacheManager, InMemoryCacheManager>();
        }
    }
}