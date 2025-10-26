using DomainLayer.Contracts;
using E_Commerce.CustomMiddleWares;
using System.Runtime.CompilerServices;

namespace E_Commerce.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task SeedDataAsync(this WebApplication app) 
        {
            var Scope = app.Services.CreateScope();

            var seed = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();

           await seed.DataseedAsync();
        }

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }
        public static IApplicationBuilder UseSwaggerMiddleWares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
