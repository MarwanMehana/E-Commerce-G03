using Azure;
using DomainLayer.Contracts;
using E_Commerce.CustomMiddleWares;
using E_Commerce.Extensions;
using E_Commerce.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.Data.Contexts;
using Persistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;
using Shared.ErrorModels;
using System.Reflection.Metadata;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region  services to the container
            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfraStructureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationServices();
            #endregion

            var app = builder.Build();

           await app.SeedDataAsync();

            #region Configure the HTTP request pipeline
            // Configure the HTTP request pipeline. 
            ///app.Use(async (RequestContext, NextMiddleWare) =>
            ///{
            ///    Console.WriteLine("Request Under Processing");
            ///    await NextMiddleWare.Invoke();
            ///    Console.WriteLine("Waiting Processing");
            ///    Console.WriteLine(RequestContext.Response.Body);
            ///});
            
            app.UseCustomExceptionMiddleWare();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddleWares();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}

 