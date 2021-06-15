using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalesTaxesSystem.BLL.Services;
using SalesTaxesSystem.BLL.Services.Contracts;
using SalesTaxesSystem.DAL.Repositories;
using SalesTaxesSystem.DAL.Repositories.Contracts;
using Serilog;
using System;
using System.IO;

namespace SalesTaxesSystem
{
    partial class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<App>();
                    services.AddTransient<ICartService, CartService>();
                    services.AddTransient<ISalesService, SalesService>();
                    services.AddTransient<IItemRepository, ItemRepository>();
                    services.AddTransient<ITaxRepository, TaxRepository>();
                })
                .Build();

            var svc = ActivatorUtilities.CreateInstance<App>(host.Services);
            svc.Run();

        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "PRODUCTION"}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

        }
    }
}
