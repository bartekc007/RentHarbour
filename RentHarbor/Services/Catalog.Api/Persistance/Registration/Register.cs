using Catalog.Persistance.Context;
using Catalog.Persistance.Repositories.MongoDb;
using Catalog.Persistance.Repositories.Psql;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using RentHarbor.MongoDb.Registration;

namespace Catalog.Persistance.Registration
{
    public static class Register
    {
        public static void RegisterPersistanceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterMongoDb(configuration);
            services.AddTransient<IRentalRepository, RentalRepository>();
            services.AddTransient<IRentalRequestRepository, RentalRequestRepository>();
            services.AddTransient<IPropertyRepository, PropertyRepository>();

            services.AddSingleton<OrderDbContext>();
            services.AddSingleton<NpgsqlConnection>(_ => new NpgsqlConnection(configuration.GetConnectionString("PsqlConnectionString")));
        }
    }
}
