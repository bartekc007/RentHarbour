using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using RentHarbor.MongoDb.Data;

namespace RentHarbor.MongoDb.Registration
{
    public static class Register
    {
        public static void RegisterMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDBConnection");
            var databaseName = configuration["MongoDBSettings:DatabaseName"];

            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);

            services.AddSingleton(database);
            services.AddScoped<ICatalogContext, CatalogContext>();
            services.AddTransient<CatalogContextSeed>();
        }
    }
}
