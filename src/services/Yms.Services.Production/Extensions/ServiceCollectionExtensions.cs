using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Yms.Services.Production.Abstractions;
using Yms.Services.Production.Concretes;

namespace Yms.Services.Production.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProductionServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            //diğer servisler
            return services;
        }
    }
}
