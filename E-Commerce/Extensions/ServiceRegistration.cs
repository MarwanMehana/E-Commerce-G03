using E_Commerce.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
        {

            services.Configure<ApiBehaviorOptions>((options) =>
            {

                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorsResponse;

            });
            return services;
        }
    }
}
