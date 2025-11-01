using Microsoft.Extensions.DependencyInjection;
using Service.MappingProfiles;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(config => config.AddProfile(new ProductProfile()), typeof(AssemplyReference).Assembly);
            //services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();
            //services.AddScoped<IServiceManager, ServiceManager>();
            services.AddKeyedScoped<IServiceManager, ServiceManager>("Lazy");
            services.AddKeyedScoped<IServiceManager, ServiceManagerWithFactoryDelegate>("FactoryDelegate");

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<Func<IProductService>>(provider => {
                return () => provider.GetRequiredService<IProductService>();
            });

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<Func<IBasketService>>(provider => {
                return () => provider.GetRequiredService<IBasketService>();
            });

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<Func<IAuthenticationService>>(provider => {
                return () => provider.GetRequiredService<IAuthenticationService>();
            });

            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<Func<IOrderService>>(provider =>
            {
                return () => provider.GetRequiredService<IOrderService>();
            });

            services.AddScoped<ICacheService, CacheService>();

            return services;
        }
    }
}
