using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Ordering.Persistance.Context;

namespace Ordering.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            EnsureDatabaseCreated(host);

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void EnsureDatabaseCreated(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    services.EnsureDatabaseCreated();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred creating the DB: {ex.Message}");
                    throw;
                }
            }
        }
    }
}
