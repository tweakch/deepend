using Deepend.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace Deepend
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = AppStartup();

            var deepend = ActivatorUtilities.CreateInstance<DeependService>(host.Services);

            deepend.Display();
            deepend.Analyze();
        }

        static void ConfigSetup(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables();
        }

        static IHost AppStartup()
        {
            var builder = new ConfigurationBuilder();
            ConfigSetup(builder);

            // defining Serilog configs
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            // Initiated the denpendency injection container 
            var host = Host.CreateDefaultBuilder()
                        .ConfigureServices((context, services) =>
                        {
                            IConfiguration config = context.Configuration;

                            services.Configure<PositionOptions>(config.GetSection(PositionOptions.Position));
                            services.Configure<DeependOptions>(config.GetSection(DeependOptions.Deepend));

                            services.AddTransient<IDataService, DeependService>();
                        })
                        .UseSerilog()
                        .Build();

            return host;
        }
    }
}
