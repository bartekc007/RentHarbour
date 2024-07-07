using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Ordering.Persistance.Context;
using Ordering.Persistance.Repositories.Mongo;
using Ordering.Persistance.Repositories.Psql;
using RentHarbor.MongoDb.Registration;

namespace Ordering.Persistance.Registration
{
    public static class Register
    {
        public static void RegisterPersistanceLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterMongoDb(configuration);
            services.AddTransient<IPropertyRepository, PropertyRepository>();
            services.AddTransient<IRentalRequestRepository, RentalRequestRepository>();
            services.AddTransient<IDocumentRepository, DocumentRepository>();

            services.AddSingleton<OrderDbContext>();
            services.AddSingleton<NpgsqlConnection>(_ => new NpgsqlConnection(configuration.GetConnectionString("PsqlConnectionString")));
            
        }
    }
}
