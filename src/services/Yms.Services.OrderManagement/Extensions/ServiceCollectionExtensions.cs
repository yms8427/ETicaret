using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Yms.Services.OrderManagement.Abstractions;
using Yms.Services.OrderManagement.Concretes;

namespace Yms.Services.OrderManagement.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderServices(this IServiceCollection services)
        {
            services.AddScoped<ICartService, CartService>();
            //diğer servisler
            return services;
        }
    }
}
