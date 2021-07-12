using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Engine;
using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Serilog;
using Microsoft.EntityFrameworkCore;

namespace consoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Build())
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

            Log.Logger.Information("Iniciando........");

            var hosts = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
               
                services.AddTransient<ISearchEngine, SearchEngine>();

                services.AddDbContext<DataContext>(opt =>
                {
                    opt.UseSqlite( context.Configuration.GetConnectionString("DefaultConnection"));
                });

            })
            .UseSerilog()
            .Build();

            using var scope  = hosts.Services.CreateScope();
            var services = scope.ServiceProvider;
            try{
                var context = services.GetRequiredService<DataContext>();
                context.Database.Migrate(); // crea la bd en caso no exista
            }
            catch(Exception e){
                  Log.Logger.Error("Un error ocurrio en la migracion {ex}",e.Message);   
            }

            var svc = ActivatorUtilities.CreateInstance<SearchEngine>(hosts.Services);

            //MainAsync(args).ConfigureAwait(false).GetAwaiter().GetResult();

            svc.search().Wait();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
               .AddEnvironmentVariables();


        }

      
    }

}
